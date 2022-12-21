using AAM.Infrastructure.DbContexts;
using AAM.Infrastructure.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace AAM.Infrastructure.Repositories;

internal class EntityRepository<T, TId> : IEntityRepository<T, TId> where T : class, IDataEntity<TId>
{
    private readonly DbSet<T> _entities;
    private readonly ApplicationDbContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public EntityRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _entities = context.Set<T>();
    }

    public virtual void Add(T entity)
    {
        Guard.Against.Null(entity, nameof(entity));
        _entities.Add(entity);
    }

    public virtual ValueTask<EntityEntry<T>> AddAsync(T entity)
    {
        Guard.Against.Null(entity, nameof(entity));
        return _entities.AddAsync(entity);
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
        _entities.AddRange(entities);
    }

    public virtual Task AddRangeAsync(IEnumerable<T> entities)
    {
        return _entities.AddRangeAsync(entities);
    }

    public virtual void Delete(T entity)
    {
        Guard.Against.Null(entity, nameof(entity));
        _entities.Remove(entity);
    }

    public virtual void DeleteRange(IEnumerable<T> entities)
    {
        Guard.Against.Null(entities, nameof(entities));
        _entities.RemoveRange(entities);
    }

    public virtual bool Exists(Expression<Func<T, bool>> predicate)
    {
        return _entities.Any(predicate);
    }

    public virtual Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return _entities.AnyAsync(predicate);
    }

    public virtual IQueryable<T> GetAll(string? includeProperties = null, bool asNoTracking = true)
    {
        if (string.IsNullOrEmpty(includeProperties))
            return GetEntities(asNoTracking);

        var props = includeProperties.Split(',');
        return props.Aggregate(GetEntities(asNoTracking), (curr, prop) => curr.Include(prop));
    }

    public virtual IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
    {
        return GetEntities().Where(predicate);
    }

    public virtual T? GetFirst(Expression<Func<T, bool>> predicate)
    {
        return GetEntities().FirstOrDefault(predicate);
    }

    public virtual Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate)
    {
        return GetEntities().FirstOrDefaultAsync(predicate);
    }

    public virtual T? GetSingle(Expression<Func<T, bool>> predicate)
    {
        return GetEntities().SingleOrDefault(predicate);
    }

    public virtual Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate)
    {
        return GetEntities().SingleOrDefaultAsync(predicate);
    }

    public virtual void Update(T entity)
    {
        Guard.Against.Null(entity, nameof(entity));
        _entities.Update(entity);
    }

    public virtual void UpdateRange(IEnumerable<T> entities)
    {
        Guard.Against.Null(entities, nameof(entities));
        foreach (var entity in entities)
        {
            Update(entity);
        }
    }

    public virtual T? Find(TId id)
    {
        return _entities.Find(id);
    }

    public virtual async Task<T?> FindAsync(TId id)
    {
        return await _entities.FindAsync(id);
    }

    public IQueryable<T> GetEntities(bool asNoTracking = true)
    {
        if (asNoTracking)
            return _entities.AsNoTracking();
        return _entities;
    }
}
