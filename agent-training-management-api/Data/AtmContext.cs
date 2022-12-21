using System.Security.Claims;

namespace AtmAPI.Data;

public class AtmContext : DbContext
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public AtmContext(
		DbContextOptions<AtmContext> options,
		IHttpContextAccessor httpContextAccessor
	) : base(options) => _httpContextAccessor = httpContextAccessor;

	public DbSet<Skill> Skills => Set<Skill>();
	public DbSet<Department> Departments => Set<Department>();
	public DbSet<Student> Students => Set<Student>();
	public DbSet<SkillReport> SkillReports => Set<SkillReport>();
	public DbSet<Lecturer> Lecturers => Set<Lecturer>();
	public DbSet<Operator> Operators => Set<Operator>();
	public DbSet<Absence> Absences => Set<Absence>();
	public DbSet<Category> Categories => Set<Category>();
	public DbSet<Class> Classes => Set<Class>();
	public DbSet<ClassLecturer> ClassLecturers => Set<ClassLecturer>();
	public DbSet<Schedule> Schedules => Set<Schedule>();
	public DbSet<Slot> Slots => Set<Slot>();
	public DbSet<ExternalInstitution> ExternalInstitutions => Set<ExternalInstitution>();
	public DbSet<ExternalInstitutionStudent> ExternalInstitutionStudents => Set<ExternalInstitutionStudent>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Student>(b =>
		{
			b.HasIndex(e => e.Code).IsUnique();
			b.Property(p => p.Code)
				.HasComputedColumnSql(
					string.Format(ModelConstants.IdentityCode, "S"),
					stored: true
				);
		});
		modelBuilder.Entity<Operator>(b =>
		{
			b.HasIndex(e => e.Code).IsUnique();
			b.Property(p => p.Code)
				.HasComputedColumnSql(
					string.Format(ModelConstants.IdentityCode, "O"),
					stored: true
				);
		});
		modelBuilder.Entity<Lecturer>(b =>
		{
			b.HasIndex(e => e.Code).IsUnique();
			b.Property(p => p.Code)
				.HasComputedColumnSql(
					string.Format(ModelConstants.IdentityCode, "L"),
					stored: true
				);
		});

		modelBuilder.Entity<Skill>(b => b.HasIndex(e => e.Name).IsUnique());
		modelBuilder.Entity<Category>(b => b.HasIndex(e => e.Name).IsUnique());

		modelBuilder.Entity<ClassLecturer>().HasKey(e => new { e.ClassId, e.LecturerId });

		modelBuilder.Entity<ExternalInstitution>(b => b.HasIndex(e => e.Name).IsUnique());
		modelBuilder
			.Entity<ExternalInstitutionStudent>()
			.HasKey(e => new { e.StudentId, e.ExternalInstitutionId });
		modelBuilder
			.Entity<ExternalInstitutionStudent>()
			.HasOne(exIns => exIns.Student)
			.WithMany(stud => stud.ExternalInstitutionStudents)
			.OnDelete(DeleteBehavior.NoAction);
		modelBuilder
			.Entity<ExternalInstitutionStudent>()
			.HasOne(exIns => exIns.ExternalInstitution)
			.WithMany(ex => ex.ExternalInstitutionStudents)
			.OnDelete(DeleteBehavior.NoAction);

		modelBuilder
			.Entity<ExternalInstitutionStudent>()
			.HasKey(e => new { e.StudentId, e.ExternalInstitutionId });

		modelBuilder.Entity<Slot>(
			b => b.HasIndex(e => new { e.DayOfWeek, e.StartAt, e.EndAt }).IsUnique()
		);

		// Schedule has a child entity "ScheduleCheckIns", which means no need for init DbSet.
		// All CRUDs is perform via an instance of schedule.
		modelBuilder.Entity<Schedule>(
			b =>
				b.OwnsMany(
					e => e.ScheduleCheckIns,
					sb => sb.HasKey(se => new { se.CheckInDate, se.ScheduleId })
				)
		);

		SeedData.Run(modelBuilder);
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		var entries = ChangeTracker
			.Entries()
			.Where(
				entry =>
					entry.Entity is EntityMetadataBase
					&& entry.State is EntityState.Added or EntityState.Modified
			)
			.ToList();

		if (!entries.Any())
			return base.SaveChangesAsync(cancellationToken);

		var claims = _httpContextAccessor.HttpContext?.User;
		claims.ThrowIfNull("Unauthorized user", "HttpContext.User");

		var modifier = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (modifier == null)
		{
			var clientClaim = claims.FindFirst("client_id");
			clientClaim.ThrowIfNull("Invalid claim", "HttpContext.User");
			clientClaim.Value.Throw().IfNotEquals("internal.m2m.client");
			modifier = "system";
		}

		foreach (var entry in entries)
		{
			var currentTimeStamp = DateTime.UtcNow;
			if (entry.Entity is EntityMetadataBase entity)
			{
				if (entry.State == EntityState.Added)
				{
					entity.UpdatedAt = currentTimeStamp;
					entity.CreatedAt = currentTimeStamp;
					entity.UpdatedBy = modifier;
					entity.CreatedBy = modifier;
				}
				else if (entry.State == EntityState.Modified)
				{
					entity.UpdatedAt = currentTimeStamp;
					entity.UpdatedBy = modifier;
				}
			}
		}

		return base.SaveChangesAsync(cancellationToken);
	}
}
