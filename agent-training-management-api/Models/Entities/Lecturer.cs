namespace AtmAPI.Models.Entities;

public class Lecturer : IdentityBase
{
	public int? DepartmentId { get; set; }

	public virtual Department? Department { get; set; }
	public virtual ICollection<ClassLecturer>? ClassLecturers { get; set; }
}
