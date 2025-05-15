using System.Linq.Expressions;

namespace LawyerAssistant.Application.Contracts.Persistence;

public interface IBaseRepository<T> where T : class
{
    Task AddAsync(T model);
    Task AddRangeAsync(List<T> model);
    void Delete(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions);
    Task Delete(int id);
    void Delete(T model);
    void DeleteRange(List<T> model);
    Task<T> Find(int id);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions);
    Task<List<T>> GetAllAsync();
    Task SaveChangesAsync();
    IEnumerable<T> SelectAll();
    IEnumerable<T> SelectAll(params Expression<Func<T, object>>[] includeExpressions);
    IQueryable<T> SelectAllAsQuerable();
    IQueryable<T> SelectAllAsQuerable(params Expression<Func<T, object>>[] includeExpressions);
    Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions);
    void Update(T model);
    void UpdateRange(List<T> model);
    IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    IQueryable<T> Where(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions);
}
