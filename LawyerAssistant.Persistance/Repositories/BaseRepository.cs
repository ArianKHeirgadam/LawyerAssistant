using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Persistance.ApplicationDbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LawyerAssistant.Persistance.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected MainDBContext _db;
    protected DbSet<T> Entities { get; set; }

    

    public virtual async Task AddAsync(T model)
    {
        await _db.AddAsync(model);
    }
    //********************************************************************************************************************
    public virtual async Task AddRangeAsync(List<T> model)
    {
        await _db.AddRangeAsync(model);
    }
    //********************************************************************************************************************
    public void DeleteRange(List<T> model)
    {
        _db.RemoveRange(model);
    }
    //********************************************************************************************************************
    public void Update(T model)
    {
        _db.Update(model);
    }
    //********************************************************************************************************************
    public void UpdateRange(List<T> model)
    {
        _db.UpdateRange(model);
    }
    //********************************************************************************************************************
    public virtual async Task Delete(int id)
    {
        var dbSet = _db.Set<T>();
        var entity = await dbSet.FindAsync(id);
        _db.Entry(entity).State = EntityState.Deleted;
    }
    //********************************************************************************************************************
    public virtual void Delete(T model)
    {
        _db.Remove(model);
    }
    //********************************************************************************************************************
    public virtual void Delete(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions)
    {
        var dbSet = _db.Set<T>();
        var query = dbSet.Where(predicate);
        if (includeExpressions.Any())
        {
            query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));
        }
        dbSet.RemoveRange(query);
    }
    //********************************************************************************************************************
    public virtual async Task<T> Find(int id)
    {
        var dbSet = _db.Set<T>();
        return await dbSet.FindAsync(id);
    }
    //********************************************************************************************************************
    public virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate)
    {
        var dbSet = _db.Set<T>();
        return dbSet.Where(predicate);
    }
    //********************************************************************************************************************
    public virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions)
    {
        var query = _db.Set<T>().Where(predicate);

        if (includeExpressions.Any())
        {
            query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));
        }

        return query;
    }
    //********************************************************************************************************************
    public virtual IEnumerable<T> SelectAll()
    {
        var dbSet = _db.Set<T>();
        return dbSet.AsEnumerable();
    }
    //********************************************************************************************************************
    public virtual IEnumerable<T> SelectAll(params Expression<Func<T, object>>[] includeExpressions)
    {
        var query = _db.Set<T>().AsQueryable();

        if (includeExpressions.Any())
        {
            query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));
        }

        return query.AsEnumerable();
    }
    //********************************************************************************************************************
    public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        var dbSet = _db.Set<T>();
        return await dbSet.SingleOrDefaultAsync(predicate);
    }
    //********************************************************************************************************************
    public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions)
    {
        var query = _db.Set<T>().Where(predicate);

        if (includeExpressions.Any())
        {
            query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));
        }
        return await query.SingleOrDefaultAsync();
    }
    //********************************************************************************************************************
    public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        var dbSet = _db.Set<T>();
        return await dbSet.FirstOrDefaultAsync(predicate);
    }
    //********************************************************************************************************************
    public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions)
    {
        var query = _db.Set<T>().Where(predicate);

        if (includeExpressions.Any())
        {
            query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));
        }
        return await query.FirstOrDefaultAsync();
    }
    //********************************************************************************************************************
    public virtual IQueryable<T> SelectAllAsQuerable()
    {
        var query = _db.Set<T>().AsQueryable();
        return query;
    }
    //********************************************************************************************************************
    public virtual IQueryable<T> SelectAllAsQuerable(params Expression<Func<T, object>>[] includeExpressions)
    {
        var query = _db.Set<T>().AsQueryable();

        if (includeExpressions.Any())
        {
            query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));
        }
        return query;
    }
    //********************************************************************************************************************
    public async Task<List<T>> GetAllAsync()
    {
        return await _db.Set<T>().ToListAsync();
    }
    //********************************************************************************************************************
    public virtual async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
    //********************************************************************************************************************
}
