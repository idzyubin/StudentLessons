using System.Text.Json;
using Confluent.Kafka;
using NotificationService.Domain.Contracts;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Extensions;
using NotificationService.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBusinessLogic();
builder.Services.Configure<NotificationServiceOptions>(
    builder.Configuration.GetSection(NotificationServiceOptions.Section));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/api/notification/send", async (ILogger<Program> logger, CancellationToken cancellationToken) =>
{
    var config = new ProducerConfig { BootstrapServers = builder.Configuration.GetConnectionString("Kafka") };
    using var producer = new ProducerBuilder<Null, string>(config).Build();
    const string notificationTopicName = "notification-group";
    try
    {
        var notification = new Notification
        {
            From = "i.a.dzyubin@yandex.ru", 
            To = "idzyubin@yahoo.com", 
            Subject = "Test Subject", 
            Body = "Test body"
        };
        var message = new Message<Null, string> { Value = JsonSerializer.Serialize(notification) };
        await producer.ProduceAsync(notificationTopicName, message, cancellationToken);
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