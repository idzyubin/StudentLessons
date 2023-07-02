using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;

namespace ProductService.Infrastructure.Managers;

/// <summary>
///     Реализация <see cref="IProductManager"/>
/// </summary>
public sealed class ProductManager : IProductManager
{
    public ValueTask<Product> CreateAsync(Product product, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}