namespace AAM.Infrastructure.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    int SaveChanges();

    int SaveChanges(bool acceptAllChangesOnSuccess);
}
