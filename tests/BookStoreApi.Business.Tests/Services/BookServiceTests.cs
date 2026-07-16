using BookStoreApi.Business.Services;
using BookStoreApi.Business.Tests.TestData;
using BookStoreApi.DataAccess.Repositories;
using BookStoreApi.Entities;
using Moq;

namespace BookStoreApi.Business.Tests.Services;

public sealed class BookServiceTests
{
    private readonly Mock<IBookRepository> _repositoryMock = new();
    private readonly BookService _service;

    public BookServiceTests()
    {
        _service = new BookService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_BookResponse_When_Book_Exists()
    {
        var id = Guid.CreateVersion7();
        var expectedBook = BookTestData.CreateBook(id);

        _repositoryMock
            .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedBook);

        var result = await _service.GetByIdAsync(id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(expectedBook.Id, result.Id);
        Assert.Equal(expectedBook.Title, result.Title);
        Assert.Equal(expectedBook.Author, result.Author);
        Assert.Equal(expectedBook.Price, result.Price);
        Assert.Equal(expectedBook.Stock, result.Stock);

        _repositoryMock.Verify(
            r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_Book_Does_Not_Exist()
    {
        var id = Guid.CreateVersion7();

        _repositoryMock
            .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Book?)null);

        var result = await _service.GetByIdAsync(id, CancellationToken.None);

        Assert.Null(result);

        _repositoryMock.Verify(
            r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Books()
    {
        var expectedBooks = BookTestData.CreateBooks(2);

        _repositoryMock
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedBooks);

        var responses = await _service.GetAllAsync(CancellationToken.None);

        var result = responses.ToList();

        Assert.Equal(expectedBooks.Count, result.Count);

        for (var i = 0; i < expectedBooks.Count; i++)
        {
            Assert.Equal(expectedBooks[i].Id, result[i].Id);
            Assert.Equal(expectedBooks[i].Title, result[i].Title);
            Assert.Equal(expectedBooks[i].Author, result[i].Author);
            Assert.Equal(expectedBooks[i].Price, result[i].Price);
            Assert.Equal(expectedBooks[i].Stock, result[i].Stock);
        }

        _repositoryMock.Verify(
            r => r.GetAllAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_Empty_Collection_When_No_Books_Exist()
    {
        _repositoryMock
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Enumerable.Empty<Book>());

        var responses = await _service.GetAllAsync(CancellationToken.None);

        var result = responses.ToList();

        Assert.NotNull(result);
        Assert.Empty(result);

        _repositoryMock.Verify(
            r => r.GetAllAsync(It.IsAny<CancellationToken>()),
            Times.Once());
    }

    [Fact]
    public async Task CreateAsync_Should_Return_Created_Book_Id()
    {
        var request = BookTestData.CreateBookRequest();
        var expectedId = Guid.CreateVersion7();

        _repositoryMock
            .Setup(r => r.CreateAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedId);

        var result = await _service.CreateAsync(request, CancellationToken.None);

        Assert.Equal(expectedId, result);

        _repositoryMock.Verify(
            r => r.CreateAsync(
                It.Is<Book>(b =>
                    b.Title == request.Title &&
                    b.Author == request.Author &&
                    b.Price == request.Price &&
                    b.Stock == request.Stock),
                It.IsAny<CancellationToken>()),
            Times.Once());
    }
}