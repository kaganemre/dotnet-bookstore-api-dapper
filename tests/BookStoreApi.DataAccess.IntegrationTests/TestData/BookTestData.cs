using BookStoreApi.Entities;

namespace BookStoreApi.DataAccess.IntegrationTests.TestData;

public static class BookTestData
{
    public static Book CreateBook()
        => new()
        {
            Title = "Clean Code",
            Author = "Robert C. Martin",
            Price = 49.99m,
            Stock = 15
        };
}