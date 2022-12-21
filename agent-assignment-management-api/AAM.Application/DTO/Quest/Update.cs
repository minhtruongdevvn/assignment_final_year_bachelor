using AAM.Infrastructure.Enumerations;

namespace AAM.Application.DTO.Quest;

internal class Update
{
    public string? Context { get; set; }
    public QuestStatus QuestStatus { get; set; }
    public Necessity Necessity { get; set; }
    public DateTime? Expired { get; set; }
    public int NumberOfAgent { get; set; } = 1;
    public Guid? CategoryId { get; set; }
}

