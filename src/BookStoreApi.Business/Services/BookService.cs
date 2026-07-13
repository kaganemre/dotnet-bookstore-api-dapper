using BookStoreApi.Business.Interfaces;
using BookStoreApi.DataAccess.Repositories;
using BookStoreApi.Entities;
using BookStoreApi.Shared.Dtos;

namespace BookStoreApi.Business.Services;

public sealed class BookService(IBookRepository repository) : IBookService
{
    private readonly IBookRepository _repository = repository;

    public async Task<IEnumerable<BookResponse>> GetAllAsync(CancellationToken ct)
    {
        var books = await _repository.GetAllAsync(ct);
        return books.Select(ToResponse);
    }

    public async Task<BookResponse?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var book = await _repository.GetByIdAsync(id, ct);
        return book is null ? null : ToResponse(book);
    }

    public Task<Guid> CreateAsync(CreateBookRequest request, CancellationToken ct)
    {
        var book = ToEntity(request);
        return _repository.CreateAsync(book, ct);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateBookRequest request, CancellationToken ct)
    {
        var book = ToEntity(id, request);
        return await _repository.UpdateAsync(book, ct);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct)
        => _repository.DeleteAsync(id, ct);

    private static BookResponse ToResponse(Book book)
        => new(book.Id, book.Title, book.Author, book.Price, book.Stock);

    private static Book ToEntity(CreateBookRequest request)
        => new() { Title = request.Title, Author = request.Author, Price = request.Price, Stock = request.Stock };

    private static Book ToEntity(Guid id, UpdateBookRequest request)
        => new() { Id = id, Title = request.Title, Author = request.Author, Price = request.Price, Stock = request.Stock };
}