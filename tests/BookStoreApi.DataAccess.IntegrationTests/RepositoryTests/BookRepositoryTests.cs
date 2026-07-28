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

    [Fact]
    public async Task GetByIdAsync_Should_Return_Book_When_Book_Exists()
    {
        // Arrange
        var cancellationToken = TestContext.Current.CancellationToken;

        await _fixture.ResetDatabaseAsync(cancellationToken);

        var book = BookTestData.CreateBook();

        var createdBookId = await _repository.CreateAsync(book, cancellationToken);

        // Act
        var result = await _repository.GetByIdAsync(createdBookId, cancellationToken);

        // Assert
        Assert.NotNull(result);

        Assert.Equal(createdBookId, result.Id);
        Assert.Equal(book.Title, result.Title);
        Assert.Equal(book.Author, result.Author);
        Assert.Equal(book.Price, result.Price);
        Assert.Equal(book.Stock, result.Stock);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_Book_Does_Not_Exist()
    {
        // Arrange
        var cancellationToken = TestContext.Current.CancellationToken;

        await _fixture.ResetDatabaseAsync(cancellationToken);

        var bookId = Guid.CreateVersion7();

        // Act
        var result = await _repository.GetByIdAsync(bookId, cancellationToken);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Books()
    {
        // Arrange
        var cancellationToken = TestContext.Current.CancellationToken;

        await _fixture.ResetDatabaseAsync(cancellationToken);

        var firstBook = BookTestData.CreateBook();

        var secondBook = BookTestData.CreateBook(
            title: "The Pragmatic Programmer",
            author: "Andrew Hunt",
            price: 59.99m,
            stock: 20);

        var firstBookId = await _repository.CreateAsync(firstBook, cancellationToken);
        var secondBookId = await _repository.CreateAsync(secondBook, cancellationToken);

        // Act
        var books = (await _repository.GetAllAsync(cancellationToken)).ToList();

        // Assert
        Assert.Equal(2, books.Count);

        var firstResult = Assert.Single(books, book => book.Id == firstBookId);

        Assert.Equal(firstBook.Title, firstResult.Title);
        Assert.Equal(firstBook.Author, firstResult.Author);
        Assert.Equal(firstBook.Price, firstResult.Price);
        Assert.Equal(firstBook.Stock, firstResult.Stock);

        var secondResult = Assert.Single(books, book => book.Id == secondBookId);

        Assert.Equal(secondBook.Title, secondResult.Title);
        Assert.Equal(secondBook.Author, secondResult.Author);
        Assert.Equal(secondBook.Price, secondResult.Price);
        Assert.Equal(secondBook.Stock, secondResult.Stock);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_Empty_Collection_When_No_Books_Exist()
    {
        // Arrange
        var cancellationToken = TestContext.Current.CancellationToken;

        await _fixture.ResetDatabaseAsync(cancellationToken);

        // Act
        var books = await _repository.GetAllAsync(cancellationToken);

        // Assert
        Assert.Empty(books);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Book_When_Book_Exists()
    {
        // Arrange
        var cancellationToken = TestContext.Current.CancellationToken;

        await _fixture.ResetDatabaseAsync(cancellationToken);

        var book = BookTestData.CreateBook();

        var bookId = await _repository.CreateAsync(book, cancellationToken);

        var updatedBook = BookTestData.CreateUpdatedBook(bookId);

        // Act
        var isUpdated = await _repository.UpdateAsync(updatedBook, cancellationToken);

        // Assert
        Assert.True(isUpdated);

        var updatedBookFromDatabase = await _repository.GetByIdAsync(bookId, cancellationToken);

        Assert.NotNull(updatedBookFromDatabase);

        Assert.Equal(updatedBook.Id, updatedBookFromDatabase.Id);
        Assert.Equal(updatedBook.Title, updatedBookFromDatabase.Title);
        Assert.Equal(updatedBook.Author, updatedBookFromDatabase.Author);
        Assert.Equal(updatedBook.Price, updatedBookFromDatabase.Price);
        Assert.Equal(updatedBook.Stock, updatedBookFromDatabase.Stock);
    }

    [Fact]
    public async Task UpdateAsync_Should_Return_False_When_Book_Does_Not_Exist()
    {
        // Arrange
        var cancellationToken = TestContext.Current.CancellationToken;

        await _fixture.ResetDatabaseAsync(cancellationToken);

        var book = BookTestData.CreateUpdatedBook(Guid.CreateVersion7());

        // Act
        var isUpdated = await _repository.UpdateAsync(book, cancellationToken);

        // Assert
        Assert.False(isUpdated);
    }

    [Fact]
    public async Task DeleteAsync_Should_Delete_Book_When_Book_Exists()
    {
        // Arrange
        var cancellationToken = TestContext.Current.CancellationToken;

        await _fixture.ResetDatabaseAsync(cancellationToken);

        var book = BookTestData.CreateBook();

        var bookId = await _repository.CreateAsync(book, cancellationToken);

        // Act
        var isDeleted = await _repository.DeleteAsync(bookId, cancellationToken);

        // Assert
        Assert.True(isDeleted);

        var deletedBook = await _repository.GetByIdAsync(bookId, cancellationToken);

        Assert.Null(deletedBook);
    }
}