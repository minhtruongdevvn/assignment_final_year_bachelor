namespace AAM.Infrastructure.Interfaces;

public interface IEntityRepository<T, TId> : IReadRepository<T>, IWriteRepository<T> where T : class, IEntity<TId>
{
    T? Find(TId id);
    Task<T?> FindAsync(TId id);
}
