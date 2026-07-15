using BookStoreApi.Entities;

namespace BookStoreApi.Business.Tests.TestData;

public static class BookTestData
{
    public static Book CreateBook(Guid? id = null)
        => new()
        {
            Id = id ?? Guid.CreateVersion7(),
            Title = "Clean Code",
            Author = "Robert C. Martin",
            Price = 49.99m,
            Stock = 15
        };
}