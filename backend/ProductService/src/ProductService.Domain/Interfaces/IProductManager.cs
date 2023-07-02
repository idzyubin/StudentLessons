using ProductService.Domain.Entities;

namespace ProductService.Domain.Interfaces;

/// <summary>
///     Сервис для управления товарами
/// </summary>
public interface IProductManager
{
    /// <summary>
    ///     Добавить новый товар
    /// </summary>
    /// <param name="product">Данные нового товара</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Данные добавленного товара</returns>
    ValueTask<Product> CreateAsync(Product product, CancellationToken cancellationToken);
}