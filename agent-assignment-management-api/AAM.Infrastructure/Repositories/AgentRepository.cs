using AAM.Infrastructure.DbContexts;
using AAM.Infrastructure.Interfaces;
using AAM.Infrastructure.Models;
using AAM.Infrastructure.Repositories;

namespace AAM.Infrastructure;

internal class AgentRepository : EntityRepository<Agent, Guid>, IAgentRepository
{
    public AgentRepository(
        ApplicationDbContext context
    ) : base(context) { }
}

