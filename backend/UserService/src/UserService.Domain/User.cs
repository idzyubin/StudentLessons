using System.ComponentModel.DataAnnotations;

namespace UserService.Domain;

/// <summary>
///     Сущность для работы с пользователем
/// </summary>
public class User
{
    /// <summary>
    ///     Идентификатор пользователя
    /// </summary>
    [Key]
    public long Id { get; set; }

    /// <summary>
    ///     Логин пользователя
    /// </summary>
    public string Login { get; set; } = "";

    /// <summary>
    ///     Пароль пользователя
    /// </summary>
    public string Password { get; set; } = "";

    /// <summary>
    ///     Имя пользователя
    /// </summary>
    public string FirstName { get; set; } = "";

    /// <summary>
    ///     Фамилия пользователя
    /// </summary>
    public string LastName { get; set; } = "";
}