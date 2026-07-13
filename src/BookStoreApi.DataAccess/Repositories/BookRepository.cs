using BookStoreApi.DataAccess.Context;
using BookStoreApi.Entities;
using Dapper;

namespace BookStoreApi.DataAccess.Repositories;

public sealed class BookRepository(IDbConnectionFactory connectionFactory) : IBookRepository
{
    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken ct)
    {
        using var connection = await connectionFactory.CreateConnectionAsync();
        const string sql = "SELECT id, title, author, price, stock FROM books";

        var command = new CommandDefinition(sql, cancellationToken: ct);
        return await connection.QueryAsync<Book>(command);
    }

    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        using var connection = await connectionFactory.CreateConnectionAsync();
        const string sql = "SELECT id, title, author, price, stock FROM books WHERE id = @Id";

        var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: ct);
        return await connection.QueryFirstOrDefaultAsync<Book>(command);
    }

    public async Task<Guid> CreateAsync(Book book, CancellationToken ct)
    {
        using var connection = await connectionFactory.CreateConnectionAsync();
        const string sql = @"
            INSERT INTO books (title, author, price, stock)
            VALUES (@Title, @Author, @Price, @Stock)
            RETURNING id";

        var command = new CommandDefinition(sql, book, cancellationToken: ct);
        return await connection.ExecuteScalarAsync<Guid>(command);
    }

    public async Task<bool> UpdateAsync(Book book, CancellationToken ct)
    {
        using var connection = await connectionFactory.CreateConnectionAsync();
        const string sql = @"
            UPDATE books
            SET title = @Title, author = @Author, price = @Price, stock = @Stock
            WHERE id = @Id";

        var command = new CommandDefinition(sql, book, cancellationToken: ct);
        var rowsAffected = await connection.ExecuteAsync(command);

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
    {
        using var connection = await connectionFactory.CreateConnectionAsync();
        const string sql = "DELETE FROM books WHERE id = @Id";

        var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: ct);
        var rowsAffected = await connection.ExecuteAsync(command);

        return rowsAffected > 0;
    }
}