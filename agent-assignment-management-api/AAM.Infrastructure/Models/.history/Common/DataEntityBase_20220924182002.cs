using AAM.Infrastructure.Models;

namespace AAM.Infrastructure.Models;

public class DataEntityBase<TId> : AuditableEntity, IDataEntity<TId>
{
    public TId Id { get; set; } = default!;
}
