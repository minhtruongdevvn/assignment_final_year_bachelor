namespace AAM.Application;
public interface IAgentSkillService
{
    Task UpsertAsync(AgentSkillDTO agentSkill);
    Task DeleteAsync(AgentSkillDTO agentSkillDTO);
    Task SaveChangeAsync();
}

