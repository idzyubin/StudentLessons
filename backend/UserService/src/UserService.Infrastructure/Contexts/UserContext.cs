using Microsoft.EntityFrameworkCore;
using UserService.Domain;

namespace UserService.Infrastructure.Contexts;

/// <summary>
///     Контекст для работы с пользователями
/// </summary>
public sealed class UserContext : DbContext
{
    /// <summary>
    ///     Пользователи
    /// </summary>
    public DbSet<User> Users => Set<User>();

    public UserContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }
}