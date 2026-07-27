using BookStoreApi.Entities;

namespace BookStoreApi.DataAccess.IntegrationTests.TestData;

public static class BookTestData
{
    public static Book CreateBook(
        string title = "Clean Code",
        string author = "Robert C. Martin",
        decimal price = 49.99m,
        int stock = 15)
        => new()
        {
            Title = title,
            Author = author,
            Price = price,
            Stock = stock
        };

    public static Book CreateUpdatedBook(Guid id)
        => new()
        {
            Id = id,
            Title = "Clean Architecture",
            Author = "Robert C. Martin",
            Price = 79.99m,
            Stock = 25
        };
}