namespace AAM.Infrastructure.Models;

public class QuestCategory : DataEntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public virtual ICollection<Quest> Quests { get; set; } = default!;
}

