using AAM.Application;
using AAM.Infrastructure.Enumerations;
using AAM.Infrastructure.Interfaces;
using AAM.Infrastructure.Models;
using AutoMapper;
using Fluorite.Strainer.Models;
using Fluorite.Strainer.Services;
using Microsoft.EntityFrameworkCore;

namespace AAM.API;

public class AgentService : PaginationService<Agent, AgentDTO>, IAgentService
{
    readonly IAgentRepository _agentRepository;
    readonly IMapper _mapper;
    public AgentService(
        IAgentRepository agentRepository, 
        IMapper mapper,
        IConfiguration config,
        IStrainerProcessor strainerProcessor
    ) : base(agentRepository.GetAll(), config, strainerProcessor, mapper)
    {
        _agentRepository = agentRepository;
        _mapper = mapper;
    }

    public async Task AddAsync(AgentDTO agent)
    {
        await _agentRepository.AddAsync(agent.GetAddModel());
    }

    public Task AddRangeAsync(IEnumerable<AgentDTO> agentDTOs)
    {
        var agents = agentDTOs.Select(agent => agent.GetAddModel());
        return _agentRepository.AddRangeAsync(agents);
    }

    public async Task UpdateAsync(AgentDTO agent)
    {
        var oldAgent = await _agentRepository
            .GetEntities(false)
            .FirstOrDefaultAsync(x => x.IdentityReference == agent.IdentityReference);
        if (oldAgent == null)
            throw new ClientException("Agent(with REF) not found", agent.IdentityReference!, ErrorType.EntityNotFound);
        var newAgent = agent.GetUpdatedModel(oldAgent);
        _agentRepository.Update(newAgent);
    }

    public async Task DeleteAsync(string id)
    {
        var deleteAgent = await _agentRepository
            .GetEntities(false)
            .FirstOrDefaultAsync(x => x.IdentityReference == id);

        if (deleteAgent == null) return;
        _agentRepository.Delete(deleteAgent);
    }

    public Task SaveChangeAsync()
    {
        return _agentRepository.UnitOfWork.SaveChangesAsync();
    }

    public async Task<List<AgentDTO>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        var agents = await _agentRepository
            .GetAll("AgentSkills,AgentSkills.Skill")
            .Where(a => ids.Contains(a.Id))
            .ToListAsync();
        return _mapper.Map<List<AgentDTO>>(agents);
    }

    public Task<bool> BusyAsync(Guid id)
    {
        return _agentRepository
            .GetAll("AgentQuests,AgentQuests.Quest")
            .AnyAsync(a => 
                a.Id == id && 
                a.AgentQuests!.Any(
                    aq => aq.Quest.QuestStatus == QuestStatus.OnGoing
                ));
    }

    public async Task<AgentDTO?> GetByIdAsync(Guid id)
    {
        var agent = await _agentRepository
            .GetAll("AgentSkills,AgentSkills.Skill")
            .FirstOrDefaultAsync(a => a.Id == id);

        if (agent == null) return null;

        return _mapper.Map<AgentDTO>(agent);
    }

    public Task<PagedResult<AgentDTO>> GetPageResultAsync(StrainerModel model)
    {
        return GeneratePageResultAsync(model);
    }
}

