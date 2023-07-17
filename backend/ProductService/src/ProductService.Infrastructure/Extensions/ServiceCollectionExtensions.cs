using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;
using ProductService.Infrastructure.Contexts;
using ProductService.Infrastructure.Managers;

namespace ProductService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, string connectionString) =>
        services
            .AddFluentValidation()
            .AddMapper()
            .AddDatabase(connectionString)
            .AddManagers();

    private static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services.AddScoped<IBaseManager<Product>, ProductManager>();
        services.AddScoped<IBaseManager<Category>, CategoryManager>();
        return services;
    }
    
    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<ProductContext>(builder => builder.UseNpgsql(connectionString));
    }
    
    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        return services.AddAutoMapper(expression => expression.AddMaps(Assembly.GetExecutingAssembly()));
    }
}