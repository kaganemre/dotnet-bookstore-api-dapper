using BookStoreApi.Dtos;
using BookStoreApi.Repositories;

namespace BookStoreApi.Endpoints;

public static class BookEndpoints
{
    public static IEndpointRouteBuilder MapBookEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/books");

        group.MapGet("/", async (IBookRepository repository) =>
            Results.Ok(await repository.GetAllAsync()));

        group.MapGet("/{id:guid}", async (Guid id, IBookRepository repository) =>
            await repository.GetByIdAsync(id) is { } book
                ? Results.Ok(book)
                : Results.NotFound());

        group.MapPost("/", async (CreateBookRequest request, IBookRepository repository) =>
        {
            var newBookId = await repository.CreateAsync(request);
            return Results.Created($"/api/books/{newBookId}", new CreatedResponse(newBookId));
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateBookRequest request, IBookRepository repository) =>
        {
            var isUpdated = await repository.UpdateAsync(id, request);
            return isUpdated ? Results.NoContent() : Results.NotFound();
        });

        group.MapDelete("/{id:guid}", async (Guid id, IBookRepository repository) =>
        {
            var isDeleted = await repository.DeleteAsync(id);
            return isDeleted ? Results.NoContent() : Results.NotFound();
        });

        return app;
    }
}