using AAM.Infrastructure.Interfaces;
using AAM.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AAM.Infrastructure.DbContexts;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    readonly IHttpContextAccessor? _httpContextAccessor;

    public DbSet<Agent> Agents { get; set; } = default!;
    public DbSet<AgentSkill> AgentSkills { get; set; } = default!;
    public DbSet<Quest> Quests { get; set; } = default!;
    public DbSet<AgentQuest> AgentQuests { get; set; } = default!;
    public DbSet<QuestCategory> QuestCategories { get; set; } = default!;
    public DbSet<Skill> Skills { get; set; } = default!;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options, 
        IHttpContextAccessor? httpContextAccessor = null
    ) : base(options) {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Quest>(b =>
        {
            b.HasIndex(e => e.Code).IsUnique();
            b.Property(p => p.Code)
                .HasComputedColumnSql(
                    @"CONCAT(
                        REPLACE(SUBSTRING(CONVERT(VARCHAR, [DateCreated], 20), 0, CHARINDEX(' ', CONVERT(VARCHAR,  [DateCreated], 20))), '-', '')
                        ,
                        '.'
                        ,
                        REPLACE(SUBSTRING(CONVERT(VARCHAR,  [DateCreated], 20), CHARINDEX(' ', CONVERT(VARCHAR,  [DateCreated], 20)) + 1,8), ':', '.')
                    )",
                    stored: true
                );
        });

        builder.Entity<Skill>(b => b.HasIndex(e => e.Name).IsUnique());
        builder.Entity<QuestCategory>(b => b.HasIndex(e => e.Name).IsUnique());
        base.OnModelCreating(builder);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        FillAuditInfo();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override int SaveChanges()
    {
        FillAuditInfo();
        return base.SaveChanges(true);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        FillAuditInfo();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        FillAuditInfo();
        return base.SaveChangesAsync(true, cancellationToken);
    }

    private void FillAuditInfo() {
        var entries = ChangeTracker.Entries<AuditableEntity>();
        DbContextUpdateOperations.UpdateDates(entries);
        DbContextUpdateOperations.UpdateModifier(entries, _httpContextAccessor!);
    }
}
