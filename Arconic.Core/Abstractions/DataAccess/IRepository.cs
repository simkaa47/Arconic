using System.Linq.Expressions;
using Arconic.Core.Infrastructure.DataContext;

namespace Arconic.Core.Abstractions.DataAccess;

public interface IRepository<T> where T : Entity
{
    Task ClearAsync();
    Task<T?> GetByIdAsync(long id);
    Task<List<T>> ListAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T?> GetFirstWhere(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
    Task UpdateAllAsync(List<T> list);
    Task<IEnumerable<T>> InitAsync(IEnumerable<T> initCollection, int requiredCount);
    Task<T?> GetLastWhere<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderExp);
}