using System.Linq.Expressions;
using Mc2.CrudTest.Application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructure.Data.Repositories;

internal abstract class BaseRepository<T> : IBaseRepository<T>
     where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entitySet;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _entitySet = _context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _entitySet.FindAsync([id], cancellationToken: cancellationToken);
    }

    public virtual async Task<IQueryable<T>> GetAllAsync(bool noTracking = true, CancellationToken cancellationToken = default)
    {
        var set = _entitySet;

        if (noTracking)
        {
            return set.AsNoTracking();
        }

        return set;
    }

    public virtual async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> criteria, bool noTracking = true, CancellationToken cancellationToken = default)
    {
        var set = _entitySet.Where(criteria);

        if (noTracking)
        {
            return set.AsNoTracking();
        }

        return set;
    }

    public virtual void Add(T entity)
    {
        _entitySet.Add(entity);
    }

    public virtual void Add(List<T> entities)
    {
        _entitySet.AddRange(entities);
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(T entity)
    {
        _entitySet.Remove(entity);
    }

    public virtual void Remove(IEnumerable<T> entitiesToRemove)
    {
        _entitySet.RemoveRange(entitiesToRemove);
    }
}
