using BookStoreApi.DataAccess.Seed;

namespace BookStoreApi.Api.Extensions;

public static class DatabaseSeedingExtensions
{
    public static async Task<bool?> HandleDatabaseSeedingAsync(this WebApplication app, string[] args)
    {
        if (!args.Contains("--seed"))
            return null;

        using var scope = app.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<IDatabaseSeeder>>();

        try
        {
            var seeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();
            await seeder.SeedAsync(1000);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "An error occurred during the database seeding process.");
            return false;
        }
    }
}