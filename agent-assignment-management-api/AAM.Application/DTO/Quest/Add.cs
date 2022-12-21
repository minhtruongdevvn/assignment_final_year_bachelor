using AAM.Infrastructure.Enumerations;

namespace AAM.Application.DTO.Quest;

internal class Add
{
    public string Context { get; set; } = string.Empty;
    public QuestStatus QuestStatus { get; set; } = QuestStatus.Waiting;
    public Necessity Necessity { get; set; } = Necessity.NeedTime;
    public DateTime? Expired { get; set; }
    public int NumberOfAgent { get; set; } = 1;
    public Guid CategoryId { get; set; }
}

