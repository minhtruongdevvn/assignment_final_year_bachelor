using AAM.Infrastructure.Enumerations;

namespace AAM.Infrastructure.Models;

public class Quest : DataEntityBase<Guid>
{
    public string Context { get; set; } = string.Empty;
    public QuestStatus QuestStatus { get; set; } = default!;
    public Guid CategoryId { get; set; }
    public QuestCategory Category { get; set; } = default!;
    public DateTime? Expired { get; set; }
    public int NumberOfAgent { get; set; } = 1;
    public string Code { get; set; } = string.Empty;
    public Necessity Necessity { get; set; } = default!;
    public virtual ICollection<AgentQuest>? AgentQuests { get; set; } = default!;
    public virtual ICollection<QuestTransaction>? QuestTransactions { get; set; } = default!;
}

