using AutoMapper;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Contexts;

namespace ProductService.Infrastructure.Managers;

/// <summary>
///     Менеджер категорий
/// </summary>
public sealed class ProductManager : BaseManager<Product>
{
    /// <inheritdoc cref="BaseManager{TEntity}"/>
    public ProductManager(ProductContext context, IMapper mapper) : base(context, mapper)
    {
    }
}