namespace AAM.Infrastructure.Models;

public class CriteriaType : DataEntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double DefaultCompleteRate { get; set; }
    public virtual ICollection<Criteria> Criterias { get; set; } = default!;
}

