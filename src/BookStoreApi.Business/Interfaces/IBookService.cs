using BookStoreApi.BookStoreApi.Business.Dtos;

namespace BookStoreApi.BookStoreApi.Business.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookResponse>> GetAllAsync(CancellationToken ct);
    Task<BookResponse?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<Guid> CreateAsync(CreateBookRequest request, CancellationToken ct);
    Task<bool> UpdateAsync(Guid id, UpdateBookRequest request, CancellationToken ct);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct);
}