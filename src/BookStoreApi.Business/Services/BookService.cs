using BookStoreApi.Business.Extensions;
using BookStoreApi.Business.Interfaces;
using BookStoreApi.DataAccess.Repositories;
using BookStoreApi.Shared.Dtos;

namespace BookStoreApi.Business.Services;

public sealed class BookService(IBookRepository repository) : IBookService
{
    private readonly IBookRepository _repository = repository;

    public async Task<IEnumerable<BookResponse>> GetAllAsync(CancellationToken ct)
    {
        var books = await _repository.GetAllAsync(ct);
        return books.Select(b => b.MapToBookResponse());
    }

    public async Task<BookResponse?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var book = await _repository.GetByIdAsync(id, ct);
        return book?.MapToBookResponse();
    }

    public Task<Guid> CreateAsync(CreateBookRequest request, CancellationToken ct)
    {
        var book = request.MapToBook();
        return _repository.CreateAsync(book, ct);
    }

    public Task<bool> UpdateAsync(Guid id, UpdateBookRequest request, CancellationToken ct)
    {
        var book = request.MapToBook(id);
        return _repository.UpdateAsync(book, ct);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct)
        => _repository.DeleteAsync(id, ct);
}