using System.Data;
using Npgsql;

namespace BookStoreApi.Data;

public sealed class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly IConfiguration _configuration = configuration;
    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        await connection.OpenAsync();

        return connection;
    }
}