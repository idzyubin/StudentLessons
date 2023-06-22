namespace UserService.Domain;

/// <summary>
///     Интерфейс взаимодействия с пользователями
/// </summary>
public interface IUserManager
{
    /// <summary>
    ///     Вернуть список всех пользователей
    /// </summary>
    /// <returns>Список всех пользователей</returns>
    List<User> GetAll();

    /// <summary>
    ///     Получить пользователя по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Данные искомого пользователя</returns>
    User? GetById(long id);

    /// <summary>
    ///     Добавить пользователя в систему
    /// </summary>
    /// <param name="user">Данные добавляемого пользователя</param>
    /// <returns>Данные добавленного пользователя</returns>
    User Create(User user);

    /// <summary>
    ///     Обновить данные пользователя в системе
    /// </summary>
    /// <param name="user">Данные обновляемого пользователя</param>
    /// <returns>Данные обновленного пользователя</returns>
    User? Update(User user);

    /// <summary>
    ///     Удалить данные пользователя из системы
    /// </summary>
    /// <param name="id">Идентификатор обновляемого пользователя</param>
    /// <returns>Данные удаленного пользователя</returns>
    User? Delete(long id);
}