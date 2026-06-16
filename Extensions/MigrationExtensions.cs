using BookStoreApi.Data;

namespace BookStoreApi.Extensions;

public static class MigrationExtensions
{
    public static async Task<bool?> HandleDatabaseSeedingAsync(this WebApplication app, string[] args)
    {
        if (!args.Contains("--seed"))
            return null;

        using var scope = app.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<DatabaseSeeder>>();

        try
        {
            var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
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