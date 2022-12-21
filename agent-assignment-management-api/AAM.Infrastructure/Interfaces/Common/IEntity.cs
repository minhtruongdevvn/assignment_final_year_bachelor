namespace AAM.Infrastructure.Interfaces
{
    public interface IEntity<TId>
    {
        TId Id { get; }
    }
}
