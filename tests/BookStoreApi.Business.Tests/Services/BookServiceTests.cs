using BookStoreApi.Business.Services;
using BookStoreApi.Business.Tests.TestData;
using BookStoreApi.DataAccess.Repositories;
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
}