namespace AAM.Infrastructure.Models;

public class Skill : DataEntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public virtual ICollection<AgentSkill> AgentSkills { get; set; } = default!;
}

