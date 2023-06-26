namespace NotificationService.Infrastructure.Options;

/// <summary>
///     Опции для работы сервиса уведомлений.
///     Получаем эти данные из файла appsettings.json
/// </summary>
public class NotificationServiceOptions
{
    public const string Section = nameof(NotificationServiceOptions);

    /// <summary>
    ///     Хост сервиса уведомлений
    /// </summary>
    public string Host { get; set; } = "";

    /// <summary>
    ///     Порт сервиса уведомлений
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    ///     Логин сервиса уведомлений
    /// </summary>
    public string Login { get; set; } = "";

    /// <summary>
    ///     Пароль сервиса уведомлений
    /// </summary>
    public string Password { get; set; } = "";
}