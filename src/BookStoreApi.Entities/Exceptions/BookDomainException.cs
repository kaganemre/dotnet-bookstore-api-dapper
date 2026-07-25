namespace BookStoreApi.Entities.Exceptions;

public sealed class BookDomainException(string message) : Exception(message);