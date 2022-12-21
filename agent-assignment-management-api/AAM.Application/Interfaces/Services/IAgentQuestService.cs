using AAM.AgentSuggestion.Entities;

namespace AAM.Application;
public interface IAgentQuestService
{
    Task<string> AddAsync(Guid questId, Guid agentId, PredictResult predictResult);
    Task DeleteAsync(Guid questId, Guid agentId);
    Task<IEnumerable<dynamic>> GetPredictAgentInQuestAsync(Guid questId);
    Task<IEnumerable<AgentDTO>> GetAgentInQuestAsync(Guid questId);
    Task<IEnumerable<QuestDTO>> GetQuestsOfAgentAsync(
        string agentRefId, string? 
        codeFilter = null,
        int? statusFilter = null
    );
    Task SaveChangeAsync();

}

