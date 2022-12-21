namespace AtmAPI.Repositories;

public class UnitOfWork : IUnitOfWork
{
	private readonly AtmContext _context;
	private readonly ISieveProcessor _sieveProc;

	public UnitOfWork(AtmContext context, ISieveProcessor sieveProc)
	{
		_context = context;
		_sieveProc = sieveProc;
	}

	private GenericRepository<Absence>? _absence;
	public GenericRepository<Absence> Absence => _absence ??= GetRepo<Absence>();

	private GenericRepository<Category>? _category;
	public GenericRepository<Category> Category => _category ??= GetRepo<Category>();

	private GenericRepository<Class>? _class;
	public GenericRepository<Class> Class => _class ??= GetRepo<Class>();

	private GenericRepository<ClassLecturer>? _classLecturer;
	public GenericRepository<ClassLecturer> ClassLecturer =>
		_classLecturer ??= GetRepo<ClassLecturer>();

	private GenericRepository<Department>? _department;
	public GenericRepository<Department> Department => _department ??= GetRepo<Department>();

	private GenericRepository<Lecturer>? _lecturer;
	public GenericRepository<Lecturer> Lecturer => _lecturer ??= GetRepo<Lecturer>();

	private GenericRepository<Operator>? _operator;
	public GenericRepository<Operator> Operator => _operator ??= GetRepo<Operator>();

	private GenericRepository<Schedule>? _schedule;
	public GenericRepository<Schedule> Schedule => _schedule ??= GetRepo<Schedule>();

	private GenericRepository<Skill>? _skill;
	public GenericRepository<Skill> Skill => _skill ??= GetRepo<Skill>();

	private GenericRepository<SkillReport>? _skillReport;
	public GenericRepository<SkillReport> SkillReport => _skillReport ??= GetRepo<SkillReport>();

	private GenericRepository<Slot>? _slot;
	public GenericRepository<Slot> Slot => _slot ??= GetRepo<Slot>();

	private GenericRepository<Student>? _student;
	public GenericRepository<Student> Student => _student ??= GetRepo<Student>();

	private GenericRepository<ExternalInstitution>? _externalInstitution;
	public GenericRepository<ExternalInstitution> ExternalInstitution =>
		_externalInstitution ??= GetRepo<ExternalInstitution>();

	private GenericRepository<ExternalInstitutionStudent>? _externalInstitutionStudent;
	public GenericRepository<ExternalInstitutionStudent> ExternalInstitutionStudent =>
		_externalInstitutionStudent ??= GetRepo<ExternalInstitutionStudent>();

	private GenericRepository<TEntity> GetRepo<TEntity>() where TEntity : class =>
		new(_context, _sieveProc);
}
