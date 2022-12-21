using System.Linq.Expressions;

namespace AAM.Infrastructure.Interfaces;

public interface IReadRepository<T> where T : class
{
    bool Exists(Expression<Func<T, bool>> predicate);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T> GetAll(string? includeProperties = null, bool asNoTracking = true);
    IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
    T? GetFirst(Expression<Func<T, bool>> predicate);
    Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate);
    T? GetSingle(Expression<Func<T, bool>> predicate);
    Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T> GetEntities(bool asNoTracking = true);
}
