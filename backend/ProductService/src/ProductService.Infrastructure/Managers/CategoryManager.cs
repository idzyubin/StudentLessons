using System.Linq.Expressions;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;

namespace ProductService.Infrastructure.Managers;

public class CategoryManager : ICategoryManager
{
    public ValueTask<IReadOnlyCollection<Category>> GetAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}