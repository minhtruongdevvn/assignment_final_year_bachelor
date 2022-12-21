namespace AAM.Infrastructure.Models;

public class Criteria : DataEntityBase<Guid>
{
    public double CompleteRate { get; set; }
    public CriteriaType CriteriaType { get; set; } = default!;
    public Quest Quest { get; set; } = default!;
}

