namespace BookStoreApi.Entities.Tests.TestData;

public static class BookTestData
{
    public static Book CreateBook()
        => new()
        {
            Id = Guid.CreateVersion7(),
            Title = "Clean Code",
            Author = "Robert C. Martin",
            Price = 49.99m,
            Stock = 15
        };
}