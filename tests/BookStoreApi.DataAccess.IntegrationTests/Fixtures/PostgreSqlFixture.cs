using Testcontainers.PostgreSql;

namespace BookStoreApi.DataAccess.IntegrationTests.Fixtures;

public sealed class PostgreSqlFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder("postgres:18.4")
        .WithDatabase("bookstore_test")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    public PostgreSqlContainer Container => _container;
    
    public string ConnectionString => _container.GetConnectionString();

    public async ValueTask InitializeAsync()
    {
        await _container.StartAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}