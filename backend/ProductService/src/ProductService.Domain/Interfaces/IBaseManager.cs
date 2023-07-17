using System.Linq.Expressions;
using ProductService.Domain.Entities;

namespace ProductService.Domain.Interfaces;

/// <summary>
///     Базовый интерфейс для сервисов-менеджеров сущностей
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IBaseManager<TEntity> where TEntity : Entity
{
    /// <summary>
    ///     Добавление новой сущности
    /// </summary>
    /// <param name="entity">Данные добавляемой сущности</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    ValueTask<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновление сущности
    /// </summary>
    /// <param name="entity">Данные обновляемой сущности</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    ValueTask<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Удаление сущности по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор удаляемой сущности</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    ValueTask<TEntity?> DeleteAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    ///     Восстановление сущности по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор удаляемой сущности</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    ValueTask<TEntity?> RestoreAsync(long id, CancellationToken cancellationToken);
    
    /// <summary>
    ///     Поиск сущности по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор искомой сущности</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    ValueTask<TEntity?> GetAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    ///     Поиск сущностей по условию
    /// </summary>
    /// <param name="predicate">Предикат для поиска сущностей</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    ValueTask<IReadOnlyCollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
}