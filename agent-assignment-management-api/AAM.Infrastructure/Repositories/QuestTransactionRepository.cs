using AAM.Infrastructure.DbContexts;
using AAM.Infrastructure.Interfaces;
using AAM.Infrastructure.Models;
using AAM.Infrastructure.Repositories;

namespace AAM.Infrastructure;

internal class QuestTransactionRepository : EntityRepository<QuestTransaction, Guid>, IQuestTransactionRepository
{
    public QuestTransactionRepository(
        ApplicationDbContext context
    ) : base(context) { }
}

