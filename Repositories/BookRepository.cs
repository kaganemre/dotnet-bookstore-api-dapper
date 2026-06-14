using BookStoreApi.Data;
using BookStoreApi.Dtos;
using Dapper;

namespace BookStoreApi.Repositories;

public sealed class BookRepository(IDbConnectionFactory connectionFactory) : IBookRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public async Task<IEnumerable<BookResponse>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        const string sql = "SELECT id, title, author, price, stock FROM books";

        return await connection.QueryAsync<BookResponse>(sql);
    }

    public async Task<BookResponse?> GetByIdAsync(Guid id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        const string sql = "SELECT id, title, author, price, stock FROM books WHERE id = @Id";

        return await connection.QueryFirstOrDefaultAsync<BookResponse>(sql, new { Id = id });
    }

    public async Task<Guid> CreateAsync(CreateBookRequest request)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        const string sql = @"
            INSERT INTO books (title, author, price, stock)
            VALUES (@Title, @Author, @Price, @Stock)
            RETURNING id";
        
        return await connection.ExecuteScalarAsync<Guid>(sql, request);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateBookRequest request)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        const string sql = @"
        UPDATE books
        SET title = @Title, author = @Author, price = @Price, stock = @Stock
        WHERE id = @Id";

        var rowsAffected = await connection.ExecuteAsync(sql, new
        {
            id,
            request.Title,
            request.Author,
            request.Price,
            request.Stock
        });

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        const string sql = "DELETE FROM books WHERE id = @Id";

        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
        
        return rowsAffected > 0;
    }
}