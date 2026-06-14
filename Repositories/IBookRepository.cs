using BookStoreApi.Dtos;
using BookStoreApi.Entities;

namespace BookStoreApi.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<BookResponse>> GetAllAsync();
    Task<BookResponse?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(CreateBookRequest request);
    Task<bool> UpdateAsync(Guid id, UpdateBookRequest request);
    Task<bool> DeleteAsync(Guid id);
}