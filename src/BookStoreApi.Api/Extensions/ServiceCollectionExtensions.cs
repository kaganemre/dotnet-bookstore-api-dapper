using BookStoreApi.Business.Interfaces;
using BookStoreApi.Business.Services;
using BookStoreApi.DataAccess.Context;
using BookStoreApi.DataAccess.Repositories;

namespace BookStoreApi.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.AddScoped<IBookRepository, BookRepository>();

        return services;
    }

    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();

        return services;
    }
}