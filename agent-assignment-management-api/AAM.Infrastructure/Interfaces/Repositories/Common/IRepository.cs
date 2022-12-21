namespace AAM.Infrastructure.Interfaces;

public interface IRepository
{
    IUnitOfWork UnitOfWork { get; }
}
