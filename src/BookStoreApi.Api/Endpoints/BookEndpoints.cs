using BookStoreApi.BookStoreApi.Business.Dtos;
using BookStoreApi.BookStoreApi.Business.Interfaces;

namespace BookStoreApi.Api.Endpoints;

public static class BookEndpoints
{
    public static IEndpointRouteBuilder MapBookEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/books");

        group.MapGet("/", async (IBookService service, CancellationToken ct) =>
            Results.Ok(await service.GetAllAsync(ct)));

        group.MapGet("/{id:guid}", async (Guid id, IBookService service, CancellationToken ct) =>
            await service.GetByIdAsync(id, ct) is { } book
                ? Results.Ok(book)
                : Results.NotFound());

        group.MapPost("/", async (CreateBookRequest request, IBookService service, CancellationToken ct) =>
        {
            var newBookId = await service.CreateAsync(request, ct);
            return Results.Created($"/api/books/{newBookId}", new CreatedResponse(newBookId));
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateBookRequest request, IBookService service, CancellationToken ct) =>
        {
            var isUpdated = await service.UpdateAsync(id, request, ct);
            return isUpdated ? Results.NoContent() : Results.NotFound();
        });

        group.MapDelete("/{id:guid}", async (Guid id, IBookService service, CancellationToken ct) =>
        {
            var isDeleted = await service.DeleteAsync(id, ct);
            return isDeleted ? Results.NoContent() : Results.NotFound();
        });

        return app;
    }
}