using BookStoreApi.DataAccess.Context;
using BookStoreApi.DataAccess.Repositories;
using BookStoreApi.DataAccess.Seed;
using Microsoft.Extensions.DependencyInjection;

namespace BookStoreApi.DataAccess.Extensions;

public static class DataAccessServiceExtensions
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<DatabaseSeeder>();

        return services;
    }
}