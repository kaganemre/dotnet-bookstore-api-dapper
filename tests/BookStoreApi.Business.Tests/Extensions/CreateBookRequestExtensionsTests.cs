using BookStoreApi.Business.Extensions;
using BookStoreApi.Shared.Dtos;

namespace BookStoreApi.Business.Tests.Extensions;

public sealed class CreateBookRequestExtensionsTests
{
    [Fact]
    public void MapToBook_Should_Map_All_Properties()
    {
        var request = new CreateBookRequest("Clean Code", "Robert C. Martin", 49.99m, 15);

        var book = request.MapToBook();

        Assert.NotNull(book);
        Assert.Equal(request.Title, book.Title);
        Assert.Equal(request.Author, book.Author);
        Assert.Equal(request.Price, book.Price);
        Assert.Equal(request.Stock, book.Stock);
    }
}