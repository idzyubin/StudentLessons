using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Contracts.Proto.Notification;
using Microsoft.Extensions.Options;
using NotificationService.Infrastructure.Constants;
using NotificationService.Infrastructure.Extensions;
using NotificationService.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBusinessLogic();

builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection("Kafka"));
builder.Services.Configure<SchemaRegistryConfig>(builder.Configuration.GetSection("SchemaRegistryConfig"));

builder.Services.Configure<NotificationServiceOptions>(
    builder.Configuration.GetSection(NotificationServiceOptions.Section));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/api/notification/send", async (
    ILogger<Program> logger, 
    IOptions<ProducerConfig> producerConfig, 
    IOptions<SchemaRegistryConfig> schemaRegistryConfig, 
    CancellationToken cancellationToken) =>
{
    using var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig.Value);
    using var producer = new ProducerBuilder<string, Notification>(producerConfig.Value)
        .SetValueSerializer(new ProtobufSerializer<Notification>(schemaRegistry))
        .Build();
    
    try
    {
        var notification = new Notification
        {
            From = "i.a.dzyubin@yandex.ru", 
            To = "idzyubin@yahoo.com", 
            Subject = "Test Subject", 
            Body = "Test notification"
        };
        var message = new Message<string, Notification> { Key = Guid.NewGuid().ToString(), Value = notification };
        await producer.ProduceAsync(KafkaTopics.NotificationTopic, message, cancellationToken);
    }
    catch (ProduceException<Null, string> e)
    {
        logger.LogError(e, "Во время отправки сообщения произошла ошибка");
    }
    
    return Results.Ok();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();