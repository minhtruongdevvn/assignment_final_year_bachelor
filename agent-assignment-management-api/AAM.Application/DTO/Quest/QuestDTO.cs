using AAM.Application.DTO.Quest;
using AAM.Infrastructure.Enumerations;
using AAM.Infrastructure.Models;
using AutoMapper;

namespace AAM.Application;

public class QuestDTO : BaseCrudDTO
{
    public string? Context { get; set; }
    public QuestStatus QuestStatus { get; set; }
    public Necessity Necessity { get; set; }
    public DateTime? Expired { get; set; }
    public Guid? CategoryId { get; set; }
    public int NumberOfAgent { get; set; }
    public int CurrentNumberOfAgent { get; set; } = 1;
    public string Code { get; set; } = string.Empty;
    public QuestCategoryDTO? Category { get; set; }
    public IEnumerable<QuestTransactionDTO>? QuestTransactions { get; set; }

    public Quest GetAddModel()
    {
        var add = mapper.Map<Add>(this);
        return mapper.Map<Quest>(add);
    }

    public Quest GetUpdatedModel(Quest quest)
    {
        var update = mapper.Map<Update>(this);
        return mapper.Map(update, quest);
    }

    protected override MapperConfiguration GetConfiguration()
    {
        return GetGenericConfiguration<Quest, QuestDTO, Update, Add>();
    }
}

