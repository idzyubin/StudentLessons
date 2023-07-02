using Microsoft.Extensions.DependencyInjection;
using ProductService.Domain.Interfaces;
using ProductService.Infrastructure.Managers;

namespace ProductService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddScoped<IProductManager, ProductManager>();
        services.AddScoped<ICategoryManager, CategoryManager>();
        return services;
    }
}