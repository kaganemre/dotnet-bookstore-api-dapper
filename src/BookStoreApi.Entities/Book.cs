using BookStoreApi.Entities.Exceptions;

namespace BookStoreApi.Entities;

public sealed class Book
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required decimal Price { get; set; }
    public required int Stock { get; set; }

    public void ChangePrice(decimal price)
    {
        if (price <= 0)
        {
            throw new BookDomainException("Price must be greater than zero");
        }

        Price = price;
    }
}