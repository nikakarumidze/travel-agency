using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;

namespace Repositories.Implementations;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _table;

    public BaseRepository(DbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }


    public IQueryable<T> Table => _table;

    public IQueryable<T> TableAsNoTracking => _table.AsNoTracking();


    public async Task<List<T>> GetAllAsync()
        {
            return await _table
                .ToListAsync();
        }

    public async Task<List<T>> GetAllNoTrackingAsync()
        {
            return await _table.AsNoTracking().ToListAsync();
        }

    public async Task<T> GetAsync(params object[] key)
        {
            return await _table.FindAsync(key);
        }

    public async Task<T> CreateAsync(T entity)
        {
            var result = await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

    public async Task UpdateAsync(T entity)
        {
            _table.Update(entity);
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    public async Task DeleteAsync(T entity)
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
        }

    public async Task SetIsDeletedAsync(T entity)
        {
            var type = entity.GetType();
            var prop = type.GetProperty("IsDeleted");
            if (prop != null) prop.SetValue(entity, value: true);
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    public async Task SetInactiveAsync(T entity)
        {
            var type = entity.GetType();
            var prop = type.GetProperty("IsActive");
            if (prop != null) prop.SetValue(entity, value: false);
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    public Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            return _table.AnyAsync(predicate);
        }

    public async Task CreateRange(IEnumerable<T> entities)
        {
             await _table.AddRangeAsync(entities);
             await _context.SaveChangesAsync();
        }

    public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
}