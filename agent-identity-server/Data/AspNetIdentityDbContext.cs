namespace AgentIdentityServer.Data;

public class AspNetIdentityDbContext : IdentityDbContext<AppUser, AppRole, int>
{
	public AspNetIdentityDbContext(DbContextOptions<AspNetIdentityDbContext> options)
		: base(options) { }

	public DbSet<AppUser> AppUsers => Set<AppUser>();
	public DbSet<AppRole> AppRoles => Set<AppRole>();
}
