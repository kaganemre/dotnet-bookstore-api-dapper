using System.Data;

namespace BookStoreApi.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}