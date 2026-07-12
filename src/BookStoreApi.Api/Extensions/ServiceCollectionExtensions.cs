using BookStoreApi.Business.Interfaces;
using BookStoreApi.Business.Services;
using BookStoreApi.DataAccess.Extensions;

namespace BookStoreApi.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        return services.AddDataAccessServices();
    }

    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();

        return services;
    }
}