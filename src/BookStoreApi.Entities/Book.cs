namespace BookStoreApi.Entities;

public sealed class Book
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required decimal Price { get; set; }
    public required int Stock { get; set; }
}