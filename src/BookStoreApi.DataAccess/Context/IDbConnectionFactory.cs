using System.Data;

namespace BookStoreApi.DataAccess.Context;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}