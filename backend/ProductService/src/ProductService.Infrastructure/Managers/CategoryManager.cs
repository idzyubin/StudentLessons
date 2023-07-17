using AutoMapper;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Contexts;

namespace ProductService.Infrastructure.Managers;

/// <summary>
///     Менеджер категорий
/// </summary>
public sealed class CategoryManager : BaseManager<Category>
{
    /// <inheritdoc cref="BaseManager{TEntity}"/>
    public CategoryManager(ProductContext context, IMapper mapper) : base(context, mapper)
    {
    }
}