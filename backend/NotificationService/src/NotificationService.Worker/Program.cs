using Confluent.Kafka;
using Confluent.SchemaRegistry;
using NotificationService.Domain.Contracts;
using NotificationService.Infrastructure.Options;
using NotificationService.Worker;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        services.Configure<NotificationServiceOptions>(configuration.GetSection(NotificationServiceOptions.Section));

        services.Configure<ConsumerConfig>(hostContext.Configuration.GetSection("Kafka"));
        services.Configure<SchemaRegistryConfig>(hostContext.Configuration.GetSection("SchemaRegistryConfig"));
        
        services.AddHostedService<Worker>();
        services.AddScoped<INotificationService, NotificationService.Infrastructure.Services.NotificationService>();
    })
    .Build()
    .Run();
