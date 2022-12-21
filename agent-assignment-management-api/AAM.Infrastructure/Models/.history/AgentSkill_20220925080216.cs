namespace AAM.Infrastructure.Models;

public class AgentSkill : DataEntityBase<Guid>
{
    public double Score { get; set; }
    public Skill Skill { get; set; } = default!;
    public Agent Agent { get; set; } = default!;
}

