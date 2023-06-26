using NotificationService.Domain.Entities;

namespace NotificationService.Domain.Contracts;

/// <summary>
///     Интерфейс для сервиса отправки уведомлений на электронную почту
/// </summary>
public interface INotificationService
{
    /// <summary>
    ///     Отправить письмо на электронную почту
    /// </summary>
    /// <param name="notification">Объект содержащий данный письма</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="ValueTask"/></returns>
    ValueTask SendAsync(Notification notification, CancellationToken cancellationToken);
}