using BookStoreApi.Entities;
using BookStoreApi.Shared.Dtos;

namespace BookStoreApi.Business.Extensions;

public static class BookExtensions
{
    public static BookResponse MapToBookResponse(this Book entity)
        => new(entity.Id, entity.Title, entity.Author, entity.Price, entity.Stock);
}