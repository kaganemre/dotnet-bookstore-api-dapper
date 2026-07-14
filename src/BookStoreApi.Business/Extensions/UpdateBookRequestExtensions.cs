using BookStoreApi.Entities;
using BookStoreApi.Shared.Dtos;

namespace BookStoreApi.Business.Extensions;

public static class UpdateBookRequestExtensions
{
    public static Book MapToBook(this UpdateBookRequest dto, Guid id)
        => new() { Id = id, Title = dto.Title, Author = dto.Author, Price = dto.Price, Stock = dto.Stock };
}