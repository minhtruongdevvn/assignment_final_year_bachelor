using AAM.Infrastructure.DbContexts;
using AAM.Infrastructure.Interfaces;
using AAM.Infrastructure.Models;
using AAM.Infrastructure.Repositories;

namespace AAM.Infrastructure;

internal class QuestRepository : EntityRepository<Quest, Guid>, IQuestRepository
{
    public QuestRepository(
        ApplicationDbContext context
    ) : base(context) { }
}

