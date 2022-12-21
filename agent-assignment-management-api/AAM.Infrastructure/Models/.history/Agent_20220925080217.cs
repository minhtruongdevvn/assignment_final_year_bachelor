namespace AAM.Infrastructure.Models;

public class Agent : DataEntityBase<Guid>
{
    public string? UserName { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FamilyName { get; set; } = string.Empty;
    public string GivenName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; } = DateTime.Today;
    public string Picture { get; set; } = string.Empty;
    public bool Sex { get; set; } // true: female, false: male
    public string IdentityReference { get; set; } = string.Empty;
    public int SelfDiscipline { get; set; }
    public double Height { get; set; }
    public double IQ { get; set; }
    public double EQ { get; set; }
    public double Stamina { get; set; }
    public double Strength { get; set; }
    public double ReactionTime { get; set; }
    public virtual ICollection<AgentSkill> AgentSkills { get; set; } = default!;
    public virtual ICollection<Quest> Quests { get; set; } = default!;
}

