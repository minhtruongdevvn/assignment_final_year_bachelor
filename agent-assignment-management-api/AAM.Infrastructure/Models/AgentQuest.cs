namespace AAM.Infrastructure.Models;

public class AgentQuest : DataEntityBase<Guid>
{
    public string? Report { get; set; }
    public float SuccessRate { get; set; }
    public bool Success { get; set; }
    public float Score { get; set; }
    public Guid QuestId { get; set; }
    public Guid AgentId { get; set; }
    public Quest Quest { get; set; } = default!;
    public Agent Agent { get; set; } = default!;
}

