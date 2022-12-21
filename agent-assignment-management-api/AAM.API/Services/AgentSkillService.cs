using AAM.Application;
using AAM.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AAM.API;

public class AgentSkillService : IAgentSkillService
{
    readonly IAgentSkillRepository _agentSkillRepository;
    readonly IAgentRepository _agentRepository;
    readonly ISkillRepository _skillRepository;
    public AgentSkillService(
        IAgentSkillRepository agentSkillRepository,
        ISkillRepository skillRepository,
        IAgentRepository agentRepository
    )
    {
        _agentSkillRepository = agentSkillRepository;
        _skillRepository = skillRepository;
        _agentRepository = agentRepository;
    }

    public async Task UpsertAsync(AgentSkillDTO agentSkill)
    {
        var skillId = (await _skillRepository
            .GetEntities()
            .SingleOrDefaultAsync(x => x.Name == agentSkill.SkillName))?.Id;

        var agentId = (await _agentRepository
            .GetEntities()
            .SingleOrDefaultAsync(x => x.IdentityReference == agentSkill.IdentityReference))?.Id;

        if (skillId == null || agentId == null)
            throw new ClientException("Skill or agent not found", agentSkill, ErrorType.EntityNotFound);

        var oldAgentSkill = await _agentSkillRepository
            .GetEntities(false)
            .SingleOrDefaultAsync(x =>
                x.Agent.Id == agentId &&
                x.Skill.Id == skillId
            );

        if (oldAgentSkill == null)
        {
            await _agentSkillRepository.AddAsync(new()
            {
                AgentId = (Guid)agentId,
                SKillId = (Guid)skillId,
                Score = agentSkill.Score ?? 0
            });
        }
        else
        {
            oldAgentSkill.Score = agentSkill.Score ?? 0;
            _agentSkillRepository.Update(oldAgentSkill);
        }
    }

    public async Task DeleteAsync(AgentSkillDTO agentSkillDTO)
    {
        var deleteAgentSkill = await _agentSkillRepository
            .GetEntities(false)
            .SingleOrDefaultAsync(x =>
                x.Agent.IdentityReference == agentSkillDTO.IdentityReference &&
                x.Skill.Name == agentSkillDTO.SkillName
            );
        if (deleteAgentSkill == null) return;
        _agentSkillRepository.Delete(deleteAgentSkill);
    }

    public Task SaveChangeAsync()
    {
        return _agentSkillRepository.UnitOfWork.SaveChangesAsync();
    }
}

