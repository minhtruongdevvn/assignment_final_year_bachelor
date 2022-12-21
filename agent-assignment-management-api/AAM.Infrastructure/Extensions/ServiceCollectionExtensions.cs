using AAM.Infrastructure.DbContexts;
using AAM.Infrastructure.DbContexts.Initializer;
using AAM.Infrastructure.Interfaces;
using AAM.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace AAM.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabasePersistence(configuration);
        services.AddRepositories();
    }

    private static void AddDatabasePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("aamdb")));
        services.AddScoped<IDbInitializerService, DbInitializerService>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IEntityRepository<,>), typeof(EntityRepository<,>));
        services.AddScoped<IAgentRepository, AgentRepository>();
        services.AddScoped<IQuestRepository, QuestRepository>();
        services.AddScoped<IAgentQuestRepository, AgentQuestRepository>();
        services.AddScoped<IQuestCategoryRepository, QuestCategoryRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<IAgentSkillRepository, AgentSkillRepository>();
        services.AddScoped<IQuestTransactionRepository, QuestTransactionRepository>();
    }
}
