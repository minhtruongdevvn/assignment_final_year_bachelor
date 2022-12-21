using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AAM.Infrastructure.Interfaces;

public interface IWriteRepository<T> : IRepository where T : class
{
    void Add(T entity);
    ValueTask<EntityEntry<T>> AddAsync(T entity);
    void AddRange(IEnumerable<T> entities);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
}

