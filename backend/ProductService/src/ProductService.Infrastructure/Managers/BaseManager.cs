using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;
using ProductService.Infrastructure.Contexts;

namespace ProductService.Infrastructure.Managers;

/// <summary>
///     Базовый менеджер для работы с сущностями Базы Данных
/// </summary>
/// <typeparam name="TEntity"><see cref="Entity"/></typeparam>
public class BaseManager<TEntity> : IBaseManager<TEntity> where TEntity : Entity
{
    private readonly IMapper _mapper;
    private readonly ProductContext _context;

    /// <inheritdoc cref="IBaseManager{TEntity}" />
    protected BaseManager(ProductContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    /// <inheritdoc />
    public async ValueTask<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var result = await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        return result.Entity;
    }

    /// <inheritdoc />
    public async ValueTask<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var updatingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == entity.Id && !x.IsDeleted, cancellationToken);
        if (updatingEntity is null)
            return null;

        _mapper.Map(entity, updatingEntity);

        var result = _context.Update(updatingEntity);
        return result.Entity;
    }

    /// <inheritdoc />
    public async ValueTask<TEntity?> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var deletingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (deletingEntity is null)
            return null;

        var result = _context.Update(deletingEntity);
        return result.Entity;
    }

    public async ValueTask<TEntity?> RestoreAsync(long id, CancellationToken cancellationToken)
    {
        var restoringEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted, cancellationToken);
        if (restoringEntity is null)
            return null;

        var result = _context.Update(restoringEntity);
        return result.Entity;
    }

    /// <inheritdoc />
    public async ValueTask<TEntity?> GetAsync(long id, CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<IReadOnlyCollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);
    }
}