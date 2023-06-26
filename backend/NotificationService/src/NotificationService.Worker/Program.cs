using NotificationService.Domain.Contracts;
using NotificationService.Infrastructure.Options;
using NotificationService.Worker;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        services.Configure<NotificationServiceOptions>(configuration.GetSection(NotificationServiceOptions.Section));

        services.AddHostedService<Worker>();
        services.AddScoped<INotificationService, NotificationService.Infrastructure.Services.NotificationService>();
    })
    .Build()
    .Run();
