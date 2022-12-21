using AAM.Infrastructure.DbContexts;
using AAM.Infrastructure.Interfaces;
using AAM.Infrastructure.Models;
using AAM.Infrastructure.Repositories;

namespace AAM.Infrastructure;

internal class SkillRepository : EntityRepository<Skill, Guid>, ISkillRepository
{
    public SkillRepository(
        ApplicationDbContext context
    ) : base(context) { }
}

