using System.Data;
using Npgsql;

namespace BookStoreApi.DataAccess.Context;

public sealed class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));

        await connection.OpenAsync();

        return connection;
    }
}