namespace BookStoreApi.BookStoreApi.Business.Dtos;

public record CreateBookRequest(string Title, string Author, decimal Price, int Stock);