using AAM.Application.DTO.Agent;
using AAM.Infrastructure.Models;
using AutoMapper;

namespace AAM.Application;

public class AgentDTO : BaseCrudDTO
{
    public string? Email { get; set; }
    public string? FamilyName { get; set; }
    public string? GivenName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Picture { get; set; }
    public bool? Sex { get; set; } // true: female, false: male
    public string? Code { get; set; }
    public string? IdentityReference { get; set; }
    public int? SelfDiscipline { get; set; }
    public int? Age { get; set; }
    public double? Height { get; set; }
    public double? IQ { get; set; }
    public double? EQ { get; set; }
    public double? Stamina { get; set; }
    public double? Strength { get; set; }
    public double? ReactionTime { get; set; }
    public IEnumerable<AgentSkillDTO>? AgentSkills { get; set; }
    //public IEnumerable<AgentQuest>? AgentQuests { get; set; }

    public Agent GetAddModel()
    {
        var add = mapper.Map<Add>(this);
        return mapper.Map<Agent>(add);
    }

    public Agent GetUpdatedModel(Agent agent)
    {
        var update = mapper.Map<Update>(this);
        return mapper.Map(update, agent);
    }

    protected override MapperConfiguration GetConfiguration()
    {
        return GetGenericConfiguration<Agent, AgentDTO, Update, Add>();
    }
}

