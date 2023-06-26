namespace NotificationService.Domain.Entities;

/// <summary>
///     Сущность письма электронной почты
/// </summary>
public class Notification
{
    /// <summary>
    ///     Отправитель письма
    /// </summary>
    public string From { get; set; } = "";

    /// <summary>
    ///     Получатель письма
    /// </summary>
    public string To { get; set; } = "";

    /// <summary>
    ///     Тема письма
    /// </summary>
    public string Subject { get; set; } = "";

    /// <summary>
    ///     Тело письма
    /// </summary>
    public string Body { get; set; } = "";
}