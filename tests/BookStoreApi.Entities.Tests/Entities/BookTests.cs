using BookStoreApi.Entities.Tests.TestData;

namespace BookStoreApi.Entities.Tests.Entities;

public sealed class BookTests
{
    [Fact]
    public void ChangePrice_Should_Update_Price_When_Price_Is_Valid()
    {
        // Arrange
        var book = BookTestData.CreateBook();

        // Act
        book.ChangePrice(79.99m);

        // Assert
        Assert.Equal(79.99m, book.Price);
    }
}