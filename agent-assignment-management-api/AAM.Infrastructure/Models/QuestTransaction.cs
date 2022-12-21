using AAM.Infrastructure.Enumerations;

namespace AAM.Infrastructure.Models;

public class QuestTransaction : DataEntityBase<Guid>
{
    public QuestStatus QuestStatus { get; set; } = default!;
    public Guid QuestId { get; set; }
    public Quest Quest { get; set; } = default!;
}

