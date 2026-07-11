namespace BookStoreApi.BookStoreApi.Business.Dtos;

public record BookResponse(Guid Id, string Title, string Author, decimal Price, int Stock);