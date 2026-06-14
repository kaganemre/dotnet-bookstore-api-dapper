namespace BookStoreApi.Entities;

public sealed class Book
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Author { get; init; }
    public decimal Price { get; init; }
    public int Stock { get; init; }
}