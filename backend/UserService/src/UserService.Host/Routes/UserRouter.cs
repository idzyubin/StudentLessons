using UserService.Domain;

namespace UserService.Host.Routes;

/// <summary>
///     Роутер для работы с пользователями
/// </summary>
public static class UserRouter
{
    /// <summary>
    ///     Добавляем роутер для работы с пользователями
    /// </summary>
    /// <param name="application">Объект приложения</param>
    /// <returns>Модифицированный объект приложения</returns>
    public static WebApplication AddUserRouter(this WebApplication application)
    {
        // Производим группировку логики.
        var userGroup = application.MapGroup("/api/users");

        userGroup.MapGet(pattern: "/", handler: GetAllUsers);
        userGroup.MapGet(pattern: "/{id:long}", handler: GetUserById);
        userGroup.MapPost(pattern: "/", handler: CreateUser);
        userGroup.MapPut(pattern: "/", handler: UpdateUser);
        userGroup.MapDelete(pattern: "/{id:long}", handler: DeleteUser);

        return application;
    }

    /// <summary>
    ///     Получить всех пользователей
    /// </summary>
    /// <returns>Список всех пользователей</returns>
    private static IResult GetAllUsers(IUserManager userManager)
    {
        var users = userManager.GetAll();
        return Results.Ok(users);
    }

    /// <summary>
    ///     Получить пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор искомого пользователя</param>
    /// <param name="userManager"><see cref="IUserManager"/></param>
    /// <returns>Данные искомого пользователя</returns>
    private static IResult GetUserById(long id, IUserManager userManager)
    {
        var user = userManager.GetById(id);
        return user is null
            ? Results.NotFound()
            : Results.Ok(user);
    }
    
    /// <summary>
    ///     Добавить пользователя в систему
    /// </summary>
    /// <param name="user">Данные добавляемого пользователя</param>
    /// <param name="userManager"><see cref="IUserManager"/></param>
    /// <returns>Данные добавленного пользователя</returns>
    private static IResult CreateUser(User user, IUserManager userManager)
    {
        var createdUser = userManager.Create(user);
        return Results.Ok(createdUser);
    }

    /// <summary>
    ///     Обновить данные пользователя в системе
    /// </summary>
    /// <param name="user">Данные обновляемого пользователя</param>
    /// <param name="userManager"><see cref="IUserManager"/></param>
    /// <returns>Данные обновленного пользователя</returns>
    private static IResult UpdateUser(User user, IUserManager userManager)
    {
        var updatedUser = userManager.Update(user);
        return updatedUser is null
            ? Results.NotFound()
            : Results.Ok(updatedUser);
    }

    /// <summary>
    ///     Удалить данные пользователя из системы
    /// </summary>
    /// <param name="id">Идентификатор обновляемого пользователя</param>
    /// <param name="userManager"><see cref="IUserManager"/></param>
    /// <returns>Данные удаленного пользователя</returns>
    private static IResult DeleteUser(long id, IUserManager userManager)
    {
        var deletedUser = userManager.Delete(id);
        return deletedUser is null
            ? Results.NotFound()
            : Results.Ok(deletedUser);
    }
}