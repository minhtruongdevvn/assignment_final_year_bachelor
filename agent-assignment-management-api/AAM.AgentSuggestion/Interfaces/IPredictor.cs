using AAM.AgentSuggestion.Entities;
using Microsoft.ML;

namespace AAM.AgentSuggestion.Interfaces;

public interface IPredictor
{
    Task<List<PredictResult>> GetAgentPredictResultAsync(string questId, bool excludeFail = true);
    List<PredictResult> GetAgentPredictResult(IDataView agentsInfo, bool excludeFail = false);
    Task<PredictResult> GetPredictResultByAgentIdAsync(string questId, string agentId);
    void Reset();

}


