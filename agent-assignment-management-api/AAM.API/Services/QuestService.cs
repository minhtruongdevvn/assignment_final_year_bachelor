using AAM.Application;
using AAM.Infrastructure.Enumerations;
using AAM.Infrastructure.Interfaces;
using AAM.Infrastructure.Models;
using AutoMapper;
using Fluorite.Strainer.Models;
using Fluorite.Strainer.Services;
using Microsoft.EntityFrameworkCore;
using Tensorflow;

namespace AAM.API;

public class QuestService : PaginationService<Quest, QuestDTO>, IQuestService
{
    readonly IQuestRepository _questRepository;
    readonly IQuestTransactionRepository _questTransactionRepository;
    readonly IQuestCategoryRepository _categoryRepository;
    readonly IMapper _mapper;
    public QuestService(
        IQuestRepository questRepository,
        IQuestCategoryRepository categoryRepository,
        IQuestTransactionRepository questTransactionRepository,
        IMapper mapper,
        IConfiguration config,
        IStrainerProcessor strainerProcessor
    ) : base(questRepository.GetAll("Category"), config, strainerProcessor, mapper)
    {
        _questRepository = questRepository;
        _categoryRepository = categoryRepository;
        _questTransactionRepository = questTransactionRepository;
        _mapper = mapper;
    }

    public async Task<Guid> AddAsync(QuestDTO quest)
    {
        var result = await _questRepository.AddAsync(quest.GetAddModel());
        await _questTransactionRepository.AddAsync(new QuestTransaction
        {
            QuestId = result.Entity.Id,
            QuestStatus = QuestStatus.Created
        });
        await _questTransactionRepository.AddAsync(new QuestTransaction
        {
            QuestId = result.Entity.Id,
            QuestStatus = result.Entity.QuestStatus
        });
        return result.Entity.Id;
    }

    public async Task<QuestDTO?> GetByIdAsync(Guid id)
    {
        var quest = await _questRepository
            .GetAll("QuestTransactions,Category,AgentQuests")
            .FirstOrDefaultAsync(x => x.Id == id);

        if (quest == null) return null;

        return _mapper.Map<QuestDTO>(quest);
    }

    public Task<Quest?> GetByCodeAsync(string code)
    {
        return _questRepository
            .GetAll("AgentQuests")
            .FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task<bool> GetQuestLockStatusAsync(Guid id, bool onGoing = false)
    {
        var quest = await _questRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        return GetQuestLockStatus(quest, onGoing);
    }

    public bool GetQuestLockStatus(Quest? quest, bool onGoing = false)
    {
        if (quest == null)
            throw new ClientException("Quest not found", ErrorType.EntityNotFound);

        if (
            quest.QuestStatus == QuestStatus.Success ||
            quest.QuestStatus == QuestStatus.Failed ||
            (onGoing && quest.QuestStatus == QuestStatus.OnGoing)
        ) return true;

        return false;
    }

    public async Task<string> UpdateAsync(Guid questId, QuestDTO dto)
    {
        var oldQuest = await _questRepository.FindAsync(questId);
        if (oldQuest == null)
            throw new ClientException("Quest not found", questId, ErrorType.EntityNotFound);

        if (oldQuest.QuestStatus != dto.QuestStatus)
        {
            await _questTransactionRepository.AddAsync(new QuestTransaction
            {
                QuestId = questId,
                QuestStatus = dto.QuestStatus
            });
        }

        var newQuest = dto.GetUpdatedModel(oldQuest);
        _questRepository.Update(newQuest);

        return newQuest.Code;
    }

    public async Task DeleteAsync(Guid questId)
    {
        var deleteQuest = await _questRepository.FindAsync(questId);
        if (deleteQuest == null) return;
        _questRepository.Delete(deleteQuest);
    }

    public async Task<IEnumerable<QuestCategoryDTO>> GetCategoriesAsync()
    {
        return _mapper.Map<IEnumerable<QuestCategoryDTO>>(
            await _categoryRepository.GetAll().ToListAsync()
        );
    }

    public Task<PagedResult<QuestDTO>> GetPageResultAsync(StrainerModel model)
    {
        return GeneratePageResultAsync(model);
    }

    public async Task<int> GetNumberOfAgentByQuest(Guid questId)
    {
        var quest = await _questRepository.FindAsync(questId);
        if(quest == null) return 0;
        return quest.NumberOfAgent;
    }

    public async Task UpdateStatusAsync(Guid questId, QuestStatus status) {
        var quest = await _questRepository.FindAsync(questId);
        if (quest == null) return;
        quest.QuestStatus = status;
        _questRepository.Update(quest);
    }

    public Task SaveChangeAsync()
    {
        return _questRepository.UnitOfWork.SaveChangesAsync();
    }
}

