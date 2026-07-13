using BookStoreApi.Entities;

namespace BookStoreApi.DataAccess.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync(CancellationToken ct = default);
    Task<Book?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Guid> CreateAsync(Book book, CancellationToken ct = default);
    Task<bool> UpdateAsync(Book book, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}