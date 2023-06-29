using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Contracts.Proto.Notification;
using Microsoft.Extensions.Options;
using NotificationService.Domain.Contracts;
using NotificationService.Infrastructure.Constants;

namespace NotificationService.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ConsumerConfig _consumerConfig;
    private readonly SchemaRegistryConfig _schemaRegistryConfig;
    private readonly IServiceProvider _serviceProvider;

    public Worker(
        ILogger<Worker> logger,
        IServiceProvider serviceProvider,
        IOptions<ConsumerConfig> consumerConfig,
        IOptions<SchemaRegistryConfig> schemaRegistryConfig)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _consumerConfig = consumerConfig.Value;
        _schemaRegistryConfig = schemaRegistryConfig.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var schemaRegistry = new CachedSchemaRegistryClient(_schemaRegistryConfig);
        using var consumer = new ConsumerBuilder<string, Notification>(_consumerConfig)
            .SetValueDeserializer(new ProtobufDeserializer<Notification>().AsSyncOverAsync())
            .Build();
        
        try
        {
            consumer.Subscribe(KafkaTopics.NotificationTopic);
            await HandleAsync(consumer, cancellationToken);
        }
        catch (ConsumeException e)
        {
            _logger.LogError(e, "При получении сообщения возникла следующая ошибка");
            consumer.Close();
        }
    }

    /// <summary>
    ///     Метод с логикой обработки входящих сообщений
    /// </summary>
    /// <param name="consumer">Объект получатель</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    private async ValueTask HandleAsync(IConsumer<string, Notification> consumer, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var message = consumer.Consume(cancellationToken);
            if (message is null)
            {
                _logger.LogError("Невозможно прочитать полученное сообщение");
                continue;
            }

            using var scope = _serviceProvider.CreateScope();
            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
    
            var value = message.Message.Value;
            var notification = new Domain.Entities.Notification
            {
                From = value.From,
                To = value.To,
                Subject = value.Subject,
                Body = value.Body
            };
            await notificationService.SendAsync(notification, cancellationToken);
        }
    }
}