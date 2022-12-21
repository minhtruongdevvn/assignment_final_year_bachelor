using AAM.AgentSuggestion.Entities;
using AAM.Application;
using AAM.Infrastructure.Enumerations;
using AAM.Infrastructure.Interfaces;
using AAM.Infrastructure.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tensorflow;

namespace AAM.API;

public class AgentQuestService : IAgentQuestService
{
    readonly IAgentQuestRepository _agentQuestRepository;
    readonly IQuestRepository _questRepository;
    readonly IMapper _mapper;
    public AgentQuestService(
        IAgentQuestRepository agentQuestRepository, 
        IQuestRepository questRepository, 
        IMapper mapper
    ) 
    {
        _questRepository = questRepository;
        _agentQuestRepository = agentQuestRepository;
        _mapper = mapper;
    }

    public async Task<string> AddAsync(Guid questId, Guid agentId, PredictResult predictResult)
    {
        var quest = await _questRepository.FindAsync(questId);
        if (quest == null) {
            throw new ClientException("Quest not found", questId, ErrorType.EntityNotFound);
        }

        if (await _agentQuestRepository.ExistsAsync(
            x => x.QuestId == questId 
            && x.AgentId == agentId)
        )
        {
            return quest.Code;
        };

        await _agentQuestRepository.AddAsync(
            new AgentQuest { 
                AgentId = agentId, 
                QuestId = questId, 
                SuccessRate = predictResult.Probability,
                Success = predictResult.Success,
                Score = predictResult.Score
            }
        );

        return quest.Code;
    }

    public async Task DeleteAsync(Guid questId, Guid agentId)
    {
        var delete = await _agentQuestRepository
            .GetAll(asNoTracking: false)
            .FirstOrDefaultAsync(x =>
                x.QuestId == questId &&
                x.AgentId == agentId
            );
        if (delete == null) return;
        _agentQuestRepository.Delete(delete);
    }

    public async Task<IEnumerable<dynamic>> GetPredictAgentInQuestAsync(Guid questId)
    {
        var agentQuests = await _agentQuestRepository
            .GetAll("Agent,Agent.AgentSkills,Agent.AgentSkills.Skill")
            .Where(x => x.QuestId == questId)
            .ToListAsync();

        return agentQuests.Select(x => new
        {
            agent = _mapper.Map<AgentDTO>(x.Agent),
            predict = new PredictResult { 
                Probability = x.SuccessRate, 
                Score = x.Score, 
                Success = x.Success 
            }
        });
    }

    public async Task<IEnumerable<AgentDTO>> GetAgentInQuestAsync(Guid questId)
    {
        var agentQuests = await _agentQuestRepository
            .GetAll("Agent")
            .Where(x => x.QuestId == questId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AgentDTO>>(agentQuests.Select(x => x.Agent));
    }

    public async Task<IEnumerable<QuestDTO>> GetQuestsOfAgentAsync(
        string agentRefId, 
        string? codeFilter = null, 
        int? statusFilter = null
    )
    {
        var agentQuests = await _agentQuestRepository
            .GetAll("Quest,Quest.Category,Agent")
            .Where(x => x.Agent.IdentityReference == agentRefId)
            .Where(e => 
                (codeFilter == null || e.Quest.Code.Contains(codeFilter)) &&
                (statusFilter == null || e.Quest.QuestStatus == (QuestStatus)statusFilter)
            )
            .Take(20)
            .ToListAsync();

        return _mapper.Map<IEnumerable<QuestDTO>>(agentQuests.Select(x => x.Quest));
    }

    public Task SaveChangeAsync()
    {
        return _agentQuestRepository.UnitOfWork.SaveChangesAsync();
    }
}

