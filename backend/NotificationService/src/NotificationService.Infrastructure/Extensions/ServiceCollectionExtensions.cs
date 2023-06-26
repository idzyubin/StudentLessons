using Microsoft.Extensions.DependencyInjection;
using NotificationService.Domain.Contracts;

namespace NotificationService.Infrastructure.Extensions;

/// <summary>
///     Метод конфигурации сервисной логики
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, Services.NotificationService>();
        return services;
    }
}