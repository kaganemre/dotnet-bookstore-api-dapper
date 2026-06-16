using BookStoreApi.Data;
using BookStoreApi.Endpoints;
using BookStoreApi.Extensions;
using BookStoreApi.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<DatabaseSeeder>();

var app = builder.Build();

if (await app.HandleDatabaseSeedingAsync(args) is { } result)
{
    Environment.Exit(result ? 0 : 1);
}

// Configure the HTTP request pipeline.
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