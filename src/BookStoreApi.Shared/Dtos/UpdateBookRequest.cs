namespace BookStoreApi.Shared.Dtos;

public record UpdateBookRequest(string Title, string Author, decimal Price, int Stock);