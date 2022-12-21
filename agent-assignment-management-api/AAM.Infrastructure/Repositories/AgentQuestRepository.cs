using AAM.Infrastructure.DbContexts;
using AAM.Infrastructure.Interfaces;
using AAM.Infrastructure.Models;
using AAM.Infrastructure.Repositories;

namespace AAM.Infrastructure;

internal class AgentQuestRepository : EntityRepository<AgentQuest, Guid>, IAgentQuestRepository
{
    public AgentQuestRepository(
        ApplicationDbContext context
    ) : base(context) { }
}

