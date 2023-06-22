using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain;
using UserService.Infrastructure.Contexts;
using UserService.Infrastructure.Managers;

namespace UserService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Добавляем бизнес-логику приложения
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddManagers();
        services.AddDatabase(configuration);
        return services;
    }

    /// <summary>
    ///     Добавление менеджеров данных.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    private static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services.AddScoped<IUserManager, UserManager>();
        return services;
    }

    /// <summary>
    ///     Добавление Базы Данных.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserContext>(builder => 
            builder.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));
        
        return services;
    }
}