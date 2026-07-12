using BookStoreApi.Api.Endpoints;
using BookStoreApi.Api.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDataAccess().AddBusiness();

var app = builder.Build();

if (await app.HandleDatabaseSeedingAsync(args) is { } result)
{
    Environment.Exit(result ? 0 : 1);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapBookEndpoints();

app.Run();