namespace BookStoreApi.Dtos;

public record UpdateBookRequest(string Title, string Author, decimal Price, int Stock);