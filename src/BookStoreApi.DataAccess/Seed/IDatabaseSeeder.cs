namespace BookStoreApi.DataAccess.Seed;

public interface IDatabaseSeeder
{
    Task SeedAsync(int count = 1000, CancellationToken ct = default);
}