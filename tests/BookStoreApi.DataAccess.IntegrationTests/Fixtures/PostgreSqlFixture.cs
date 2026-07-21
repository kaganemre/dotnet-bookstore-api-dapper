using Dapper;
using Npgsql;
using Testcontainers.PostgreSql;

namespace BookStoreApi.DataAccess.IntegrationTests.Fixtures;

public sealed class PostgreSqlFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder("postgres:18.4")
        .WithDatabase("bookstore_test")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    public string ConnectionString => _container.GetConnectionString();

    public async ValueTask InitializeAsync()
    {
        await _container.StartAsync();

        var schemaPath = Path.Combine("Scripts", "schema.sql");
        var sql = await File.ReadAllTextAsync(schemaPath);

        await using var connection = new NpgsqlConnection(ConnectionString);

        await connection.OpenAsync();

        await connection.ExecuteAsync(sql);
    }

    public async Task ResetDatabaseAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);

        await connection.OpenAsync(cancellationToken);

        await connection.ExecuteAsync("""TRUNCATE TABLE books;""");
    }

    public async ValueTask DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}