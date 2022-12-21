using Fluorite.Strainer.Models;

namespace AAM.Application;
public interface IAgentService
{
    Task AddAsync(AgentDTO agent);
    Task AddRangeAsync(IEnumerable<AgentDTO> agentDTOs);
    Task UpdateAsync(AgentDTO agent);
    Task<List<AgentDTO>> GetByIdsAsync(IEnumerable<Guid> ids);
    Task DeleteAsync(string id);
    Task<PagedResult<AgentDTO>> GetPageResultAsync(StrainerModel model);
    Task<AgentDTO?> GetByIdAsync(Guid id);
    Task<bool> BusyAsync(Guid id);
    Task SaveChangeAsync();
}

