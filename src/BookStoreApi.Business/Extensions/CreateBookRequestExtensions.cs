using BookStoreApi.Entities;
using BookStoreApi.Shared.Dtos;

namespace BookStoreApi.Business.Extensions;

public static class CreateBookRequestExtensions
{
    public static Book MapToBook(this CreateBookRequest dto)
        => new() { Title = dto.Title, Author = dto.Author, Price = dto.Price, Stock = dto.Stock };
}