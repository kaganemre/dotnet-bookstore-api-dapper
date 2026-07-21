using BookStoreApi.DataAccess.IntegrationTests.Fixtures;
using Npgsql;

namespace BookStoreApi.DataAccess.IntegrationTests.SmokeTests;

public sealed class PostgreSqlFixtureTests(PostgreSqlFixture fixture) : IClassFixture<PostgreSqlFixture>
{
    [Fact]
    public async Task PostgreSql_Container_Should_Accept_Connections()
    {
        var ct = CancellationToken.None;

        await using var connection = new NpgsqlConnection(fixture.ConnectionString);

        await connection.OpenAsync(ct);

        await using var command = new NpgsqlCommand("SELECT 1", connection);

        var result = await command.ExecuteScalarAsync(ct);

        Assert.IsType<int>(result);
        Assert.Equal(1, (int)result);
    }
}