using System.Text.Json;
using Confluent.Kafka;
using NotificationService.Domain.Contracts;
using NotificationService.Domain.Entities;

namespace NotificationService.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ConsumerConfig _configuration;
    private readonly IServiceProvider _serviceProvider;

    private const string NotificationTopicName = "notification-group";

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _configuration = new ConsumerConfig
        {
            GroupId = NotificationTopicName,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            BootstrapServers = configuration.GetConnectionString("Kafka"),
        };
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var consumer = new ConsumerBuilder<Ignore, string>(_configuration).Build();
        consumer.Subscribe(NotificationTopicName);
        
        try
        {
            await HandleAsync(consumer, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
        }
    }

    /// <summary>
    ///     Метод с логикой обработки входящих сообщений
    /// </summary>
    /// <param name="consumer">Объект получатель</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    private async ValueTask HandleAsync(IConsumer<Ignore, string> consumer, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var message = consumer.Consume(cancellationToken);
            if (message is null)
            {
                _logger.LogError("Невозможно прочитать полученное сообщение");
                continue;
            }
                
            var notification = JsonSerializer.Deserialize<Notification>(message.Message.Value);
            if (notification is null)
            {
                _logger.LogError("Не удалось десериализовать полученное сообщение");
                continue;
            }

            using var scope = _serviceProvider.CreateScope();
            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
            await notificationService.SendAsync(notification, cancellationToken);
        }
    }
}