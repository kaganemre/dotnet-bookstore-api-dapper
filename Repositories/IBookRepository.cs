using BookStoreApi.Dtos;

namespace BookStoreApi.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<BookResponse>> GetAllAsync(CancellationToken ct = default);
    Task<BookResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Guid> CreateAsync(CreateBookRequest request, CancellationToken ct = default);
    Task<bool> UpdateAsync(Guid id, UpdateBookRequest request, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}