using System.Linq.Expressions;
using ProductService.Domain.Entities;

namespace ProductService.Domain.Interfaces;

public interface ICategoryManager
{
    ValueTask<IReadOnlyCollection<Category>> GetAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken);
}