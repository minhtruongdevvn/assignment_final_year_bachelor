namespace AtmAPI.Models.Entities;

public class Department : EntityBase
{
	public string Name { get; set; } = default!;
	public string? Description { get; set; }

	public virtual ICollection<Lecturer>? Lecturers { get; set; }
}
