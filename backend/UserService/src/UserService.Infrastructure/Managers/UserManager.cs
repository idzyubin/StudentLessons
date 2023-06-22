using UserService.Domain;
using UserService.Infrastructure.Contexts;

namespace UserService.Infrastructure.Managers;

/// <summary>
///     Реализация интерфейса <see cref="IUserManager"/>
/// </summary>
public class UserManager : IUserManager
{
    private readonly UserContext _context;

    /// <inheritdoc cref="IUserManager" />
    public UserManager(UserContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public List<User> GetAll()
    {
        return _context.Users.ToList();
    }

    /// <inheritdoc />
    public User? GetById(long id)
    {
        return _context.Users.FirstOrDefault(x => x.Id == id);
    }

    /// <inheritdoc />
    public User Create(User user)
    {
        var entry = _context.Add(user);
        _context.SaveChanges();
        return entry.Entity;
    }

    /// <inheritdoc />
    public User? Update(User user)
    {
        var existingUser = _context.Users.FirstOrDefault(x => x.Id == user.Id);
        if (existingUser is null)
        {
            return null;
        }

        existingUser.Login = user.Login;
        existingUser.Password = user.Password;
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;

        var entry = _context.Update(user);
        _context.SaveChanges();
        return entry.Entity;
    }

    /// <inheritdoc />
    public User? Delete(long id)
    {
        var existingUser = _context.Users.FirstOrDefault(x => x.Id == id);
        if (existingUser is null)
        {
            return null;
        }
        
        var entry = _context.Remove(existingUser);
        _context.SaveChanges();
        return entry.Entity;
    }
}