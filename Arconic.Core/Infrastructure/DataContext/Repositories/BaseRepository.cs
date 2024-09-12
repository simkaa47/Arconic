using System.Linq.Expressions;
using Arconic.Core.Abstractions.DataAccess;
using Arconic.Core.Infrastructure.DataContext.Data;
using Microsoft.EntityFrameworkCore;

namespace Arconic.Core.Infrastructure.DataContext.Repositories;

public class BaseRepository<T>(ArconicDbContext dbContext) : IRepository<T>
    where T : Entity
{
    public async Task<T> AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<T?> GetByIdAsync(long id)
    {
        var entity = await dbContext.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);
        return entity;
    }

    public async Task<T?> GetFirstWhere(Expression<Func<T, bool>> predicate)
    {
        var entity = await dbContext.Set<T>().FirstOrDefaultAsync(predicate);

        return entity;
    }

    public async Task<List<T>> GetWhere(Expression<Func<T, bool>> predicate)
    {
        var list = await dbContext.Set<T>()
            .Where(predicate)
            .ToListAsync();
        return list;
    }

    public async Task<T?> GetLastWhere<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderExp)
    {
        return await dbContext.Set<T>()
            .OrderBy(orderExp)
            .LastOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<T>> InitAsync(IEnumerable<T> initCollection, int requiredCount)
    {
        var entities = await dbContext.Set<T>().ToListAsync();
        if (entities.Count < requiredCount)
        {
            var initAsync = initCollection as T[] ?? initCollection.ToArray();
            await dbContext.Set<T>().AddRangeAsync(initAsync);
            await dbContext.SaveChangesAsync();
            return initAsync;
        }
        return entities;
    }

    public async Task<List<T>> ListAllAsync()
    {
        return await dbContext.Set<T>()
            .ToListAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAllAsync(List<T> list)
    {
        foreach (var entity in list)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
        await dbContext.SaveChangesAsync();
    }

    public async Task ClearAsync()
    {
        var entities = dbContext.Set<T>().ToArray();
        dbContext.Set<T>().RemoveRange(entities);
        await dbContext.SaveChangesAsync();
    }
}