using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Contexts;

/// <summary>
///     Контекст для работы с сущностями сервиса
/// </summary>
public class ProductContext : DbContext
{
    /// <summary>
    ///     Интерфейс для работы с товарами
    /// </summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>
    ///     Интерфейс для работы с категориями товаров
    /// </summary>
    public DbSet<Category> Categories => Set<Category>();

    /// <inheritdoc cref="DbContext"/>
    public ProductContext(DbContextOptions options) : base(options)
    {
    }
}