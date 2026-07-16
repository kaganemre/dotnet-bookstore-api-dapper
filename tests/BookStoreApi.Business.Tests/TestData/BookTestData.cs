using BookStoreApi.Entities;
using BookStoreApi.Shared.Dtos;

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

    public static IReadOnlyList<Book> CreateBooks(int count)
        => Enumerable.Range(1, count).Select(_ => CreateBook()).ToList();

    public static CreateBookRequest CreateBookRequest() 
        => new("Clean Architecture", "Robert C. Martin", 59.99m, 20);
    public static UpdateBookRequest CreateUpdateBookRequest()
        => new(
            "Clean Architecture (2nd Edition)",
            "Robert C. Martin",
            64.99m,
            25);
}