using BookStoreApi.DataAccess.Context;
using BookStoreApi.DataAccess.IntegrationTests.Fixtures;
using BookStoreApi.DataAccess.IntegrationTests.TestData;
using BookStoreApi.DataAccess.Repositories;
using BookStoreApi.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace BookStoreApi.DataAccess.IntegrationTests.RepositoryTests;

public sealed class BookRepositoryTests : IClassFixture<PostgreSqlFixture>
{
    private readonly PostgreSqlFixture _fixture;
    private readonly BookRepository _repository;

    public BookRepositoryTests(PostgreSqlFixture fixture)
    {
        _fixture = fixture;

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
            [
                new("ConnectionStrings:DefaultConnection", fixture.ConnectionString)
            ])
            .Build();

        var connectionFactory = new DbConnectionFactory(configuration);

        _repository = new BookRepository(connectionFactory);
    }

    [Fact]
    public async Task CreateAsync_Should_Insert_Book()
    {
        // Arrange
        var cancellationToken = TestContext.Current.CancellationToken;

        await _fixture.ResetDatabaseAsync(cancellationToken);

        var book = BookTestData.CreateBook();

        // Act
        var id = await _repository.CreateAsync(book, cancellationToken);

        // Assert
        Assert.NotEqual(Guid.Empty, id);

        await using var connection = new NpgsqlConnection(_fixture.ConnectionString);

        await connection.OpenAsync(cancellationToken);

        var insertedBook = await connection.QuerySingleAsync<Book>(
            """
            SELECT
                id,
                title,
                author,
                price,
                stock
            FROM books
            WHERE id = @Id;
            """,
            new { Id = id });

        Assert.Equal(id, insertedBook.Id);
        Assert.Equal(book.Title, insertedBook.Title);
        Assert.Equal(book.Author, insertedBook.Author);
        Assert.Equal(book.Price, insertedBook.Price);
        Assert.Equal(book.Stock, insertedBook.Stock);
    }
}