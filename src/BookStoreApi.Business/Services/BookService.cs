using BookStoreApi.Business.Interfaces;
using BookStoreApi.DataAccess.Repositories;
using BookStoreApi.Shared.Dtos;

namespace BookStoreApi.Business.Services;

public sealed class BookService(IBookRepository repository) : IBookService
{
    private readonly IBookRepository _repository = repository;

    public Task<IEnumerable<BookResponse>> GetAllAsync(CancellationToken ct)
        => _repository.GetAllAsync(ct);

    public Task<BookResponse?> GetByIdAsync(Guid id, CancellationToken ct)
        => _repository.GetByIdAsync(id, ct);

    public Task<Guid> CreateAsync(CreateBookRequest request, CancellationToken ct)
        => _repository.CreateAsync(request, ct);

    public Task<bool> UpdateAsync(Guid id, UpdateBookRequest request, CancellationToken ct)
        => _repository.UpdateAsync(id, request, ct);

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct)
        => _repository.DeleteAsync(id, ct);
}