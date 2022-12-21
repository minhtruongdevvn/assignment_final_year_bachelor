using AAM.Infrastructure.DbContexts;
using AAM.Infrastructure.Interfaces;
using AAM.Infrastructure.Models;
using AAM.Infrastructure.Repositories;

namespace AAM.Infrastructure;

internal class AgentSkillRepository : EntityRepository<AgentSkill, Guid>, IAgentSkillRepository
{
    public AgentSkillRepository(
        ApplicationDbContext context
    ) : base(context) { }
}

