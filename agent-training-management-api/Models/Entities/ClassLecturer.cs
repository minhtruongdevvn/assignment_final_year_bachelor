namespace AtmAPI.Models.Entities;

public class ClassLecturer : EntityMetadataBase
{
	public int ClassId { get; set; }
	public int LecturerId { get; set; }

	public virtual Class Class { get; set; } = default!;
	public virtual Lecturer Lecturer { get; set; } = default!;
}
