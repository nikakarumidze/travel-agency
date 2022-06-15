using System.Linq.Expressions;

namespace Repositories.Abstractions;

public interface IBaseRepository<T> where T:class
{
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllNoTrackingAsync();
    Task<T> GetAsync(params object[] key);
    Task<T> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task SetIsDeletedAsync(T entity);
    Task<bool> Exists(Expression<Func<T,bool>> predicate);
    Task CreateRange(IEnumerable<T> entities);
    Task SetInactiveAsync(T entity);
    Task SaveChanges();

    IQueryable<T> Table { get; }
    IQueryable<T> TableAsNoTracking { get; }
}