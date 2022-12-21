using AAM.Infrastructure.DbContexts;
using AAM.Infrastructure.Interfaces;
using AAM.Infrastructure.Models;
using AAM.Infrastructure.Repositories;

namespace AAM.Infrastructure;

internal class QuestCategoryRepository : EntityRepository<QuestCategory, Guid>, IQuestCategoryRepository
{
    public QuestCategoryRepository(
        ApplicationDbContext context
    ) : base(context) { }
}

