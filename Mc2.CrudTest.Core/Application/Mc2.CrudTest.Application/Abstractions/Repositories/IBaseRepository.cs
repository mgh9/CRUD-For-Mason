using System.Linq.Expressions;

namespace Mc2.CrudTest.Application.Abstractions.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IQueryable<T>> GetAllAsync(bool noTracking = true, CancellationToken cancellationToken = default);
        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> criteria, bool noTracking = true, CancellationToken cancellationToken = default);
        
        void Add(T entity);
        void Add(List<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Remove(IEnumerable<T> entitiesToRemove);
    }
}
