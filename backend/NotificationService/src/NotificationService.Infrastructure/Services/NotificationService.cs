using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using NotificationService.Domain.Contracts;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Options;

namespace NotificationService.Infrastructure.Services;

/// <summary>
///     Реализация интерфейса <see cref="INotificationService"/>
/// </summary>
public class NotificationService : INotificationService
{
    private readonly NotificationServiceOptions _options;

    /// <inheritdoc cref="INotificationService"/>
    public NotificationService(IOptions<NotificationServiceOptions> options)
    {
        _options = options.Value;
    }

    /// <inheritdoc />
    public async ValueTask SendAsync(Notification notification, CancellationToken cancellationToken)
    {
        using var email = new MimeMessage();
        
        email.From.Add(new MailboxAddress("Study Dev Store", notification.From));
        email.To.Add(new MailboxAddress("", notification.To));

        email.Subject = notification.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = notification.Body };

        using var client = new SmtpClient();

        await client.ConnectAsync(_options.Host, _options.Port, true, cancellationToken);
        await client.AuthenticateAsync(_options.Login, _options.Password, cancellationToken);
        await client.SendAsync(email, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);
    }
}