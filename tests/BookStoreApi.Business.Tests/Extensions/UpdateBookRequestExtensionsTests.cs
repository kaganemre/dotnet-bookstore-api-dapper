using BookStoreApi.Business.Extensions;
using BookStoreApi.Shared.Dtos;

namespace BookStoreApi.Business.Tests.Extensions;

public sealed class UpdateBookRequestExtensionsTests
{
    [Fact]
    public void MapToBook_Should_Map_All_Properties()
    {
        var id = Guid.CreateVersion7();

        var request = new UpdateBookRequest("Clean Architecture", "Robert C. Martin", 59.99m, 20);

        var book = request.MapToBook(id);

        Assert.NotNull(book);
        Assert.Equal(id, book.Id);
        Assert.Equal(request.Title, book.Title);
        Assert.Equal(request.Author, book.Author);
        Assert.Equal(request.Price, book.Price);
        Assert.Equal(request.Stock, book.Stock);
    }
}