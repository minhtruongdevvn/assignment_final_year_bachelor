namespace AtmAPI.Repositories;

public interface IUnitOfWork
{
	GenericRepository<Absence> Absence { get; }
	GenericRepository<Category> Category { get; }
	GenericRepository<Class> Class { get; }
	GenericRepository<ClassLecturer> ClassLecturer { get; }
	GenericRepository<Department> Department { get; }
	GenericRepository<Lecturer> Lecturer { get; }
	GenericRepository<Operator> Operator { get; }
	GenericRepository<Schedule> Schedule { get; }
	GenericRepository<Skill> Skill { get; }
	GenericRepository<SkillReport> SkillReport { get; }
	GenericRepository<Slot> Slot { get; }
	GenericRepository<Student> Student { get; }
	GenericRepository<ExternalInstitution> ExternalInstitution { get; }
	GenericRepository<ExternalInstitutionStudent> ExternalInstitutionStudent { get; }
}
