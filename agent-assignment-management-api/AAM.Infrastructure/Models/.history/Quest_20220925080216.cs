using AAM.Infrastructure.Models;

namespace AAM.Infrastructure.Models;

public class Quest : DataEntityBase<Guid>
{
    public string Context { get; set; } = string.Empty;
    public QuestStatus QuestStatus { get; set; } = default!;
    public double? TotalCompleteRate { get; set; }
    public string? Report { get; set; }
    public QuestCategory Category { get; set; } = default!;
    public Agent Agent { get; set; } = default!;
    public virtual ICollection<Criteria> Criterias { get; set; } = default!;
}

