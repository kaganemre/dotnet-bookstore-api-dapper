using Bogus;
using BookStoreApi.DataAccess.Repositories;
using BookStoreApi.Shared.Dtos;
using Microsoft.Extensions.Logging;

namespace BookStoreApi.DataAccess.Seed;

public sealed class DatabaseSeeder(IBookRepository bookRepository, ILogger<DatabaseSeeder> logger)
    : IDatabaseSeeder
{
    private const int BatchSize = 50;
    private const int MaxConcurrency = 10;

    public async Task SeedAsync(int count = 1000, CancellationToken ct = default)
    {
        if (count <= 0)
        {
            logger.LogWarning("Invalid count value: {Count}. Seeding operation skipped.", count);
            return;
        }

        logger.LogInformation("Generating {Count} fake book records...", count);

        var bookFaker = new Faker<CreateBookRequest>("en_GB")
            .CustomInstantiator(f => new CreateBookRequest(
                Title: f.Commerce.ProductName(),
                Author: f.Name.FullName(),
                Price: Math.Round(f.Random.Decimal(5m, 45m), 2),
                Stock: f.Random.Number(5, 150)
            ));

        var fakeBooks = bookFaker.Generate(count);

        logger.LogInformation(
            "Records generated. Transferring to Supabase in batches of {BatchSize}...",
            BatchSize);

        var batches = fakeBooks.Chunk(BatchSize);
        var totalInserted = 0;
        var totalFailed = 0;

        using var semaphore = new SemaphoreSlim(MaxConcurrency);

        foreach (var batch in batches)
        {
            ct.ThrowIfCancellationRequested();

            var tasks = batch.Select(book => InsertWithSemaphoreAsync(book, semaphore, ct));
            var results = await Task.WhenAll(tasks);

            totalInserted += results.Count(r => r);
            totalFailed += results.Count(r => !r);
        }

        logger.LogInformation(
            "Seeding complete. Successful: {Inserted}, Failed: {Failed}",
            totalInserted, totalFailed);
    }

    private async Task<bool> InsertWithSemaphoreAsync(
        CreateBookRequest book,
        SemaphoreSlim semaphore,
        CancellationToken ct)
    {
        await semaphore.WaitAsync(ct);
        try
        {
            return await InsertSafeAsync(book, ct);
        }
        finally
        {
            semaphore.Release();
        }
    }

    private async Task<bool> InsertSafeAsync(CreateBookRequest book, CancellationToken ct)
    {
        try
        {
            await bookRepository.CreateAsync(book, ct);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while inserting record: {Title}", book.Title);
            return false;
        }
    }
}