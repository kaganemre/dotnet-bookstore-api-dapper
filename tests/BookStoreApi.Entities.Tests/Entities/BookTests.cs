using BookStoreApi.Entities.Exceptions;
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

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ChangePrice_Should_Throw_When_Price_Is_Invalid(decimal price)
    {
        // Arrange
        var book = BookTestData.CreateBook();

        // Act & Assert
        var exception = Assert.Throws<BookDomainException>(() => book.ChangePrice(price));

        Assert.Equal("Price must be greater than zero.", exception.Message);
    }

    [Fact]
    public void ChangePrice_Should_Not_Modify_Price_When_Exception_Is_Thrown()
    {
        // Arrange
        var book = BookTestData.CreateBook();

        decimal originalPrice = book.Price;

        // Act
        Assert.Throws<BookDomainException>(() => book.ChangePrice(0));

        // Assert
        Assert.Equal(originalPrice, book.Price);
    }
}