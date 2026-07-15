using BookStoreApi.Business.Extensions;
using BookStoreApi.Entities;

namespace BookStoreApi.Business.Tests.Extensions;

public sealed class BookExtensionsTests
{
    [Fact]
    public void MapToBookResponse_Should_Map_All_Properties()
    {
        var book = new Book
        {
            Id = Guid.CreateVersion7(),
            Title = "Clean Code",
            Author = "Robert C. Martin",
            Price = 49.99m,
            Stock = 15
        };

        var response = book.MapToBookResponse();
        
        Assert.Equal(book.Id, response.Id);
        Assert.Equal(book.Title, response.Title);
        Assert.Equal(book.Author, response.Author);
        Assert.Equal(book.Price, response.Price);
        Assert.Equal(book.Stock, response.Stock);
    }
}