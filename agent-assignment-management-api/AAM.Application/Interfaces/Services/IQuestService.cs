using AAM.Infrastructure.Enumerations;
using AAM.Infrastructure.Models;
using Fluorite.Strainer.Models;

namespace AAM.Application;
public interface IQuestService
{
    Task<Guid> AddAsync(QuestDTO quest);
    Task<QuestDTO?> GetByIdAsync(Guid id);
    Task<IEnumerable<QuestCategoryDTO>> GetCategoriesAsync();
    Task<PagedResult<QuestDTO>> GetPageResultAsync(StrainerModel model);
    Task<string> UpdateAsync(Guid questId, QuestDTO dto);
    Task UpdateStatusAsync(Guid questId, QuestStatus status);
    Task DeleteAsync(Guid questId);
    Task<int> GetNumberOfAgentByQuest(Guid questId);
    Task<bool> GetQuestLockStatusAsync(Guid id, bool onGoing = false);
    Task<Quest?> GetByCodeAsync(string Code);
    bool GetQuestLockStatus(Quest? quest, bool onGoing = false);
    Task SaveChangeAsync();
    /*    Task AddRangeAsync(IEnumerable<AgentDTO> questDTOs);
        Task UpdateAsync(AgentDTO quest);
        Task DeleteAsync(Guid id);
        Task SaveChangeAsync();*/
}

