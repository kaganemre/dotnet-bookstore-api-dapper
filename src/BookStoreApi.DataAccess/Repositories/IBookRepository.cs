using BookStoreApi.Entities;

namespace BookStoreApi.DataAccess.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync(CancellationToken ct);
    Task<Book?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<Guid> CreateAsync(Book book, CancellationToken ct);
    Task<bool> UpdateAsync(Book book, CancellationToken ct);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct);
}