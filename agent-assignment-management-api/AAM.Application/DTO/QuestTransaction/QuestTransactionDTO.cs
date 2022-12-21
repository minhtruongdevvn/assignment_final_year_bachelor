using AAM.Infrastructure.Enumerations;
namespace AAM.Application;

public class QuestTransactionDTO : BaseDTO
{
    public QuestStatus QuestStatus { get; set; } = default!;
}

