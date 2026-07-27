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
}