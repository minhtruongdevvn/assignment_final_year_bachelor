namespace AtmAPI.Models.Entities;

public class Absence : EntityBase
{
	public Absence() { }

	public Absence(int scheduleId, int studentId, DateTime absenceDate, string? reasons = null)
	{
		ScheduleId = scheduleId;
		StudentId = studentId;
		AbsenceDate = absenceDate;
		Reasons = reasons;
	}

	public int ScheduleId { get; set; }
	public int StudentId { get; set; }
	public DateTime AbsenceDate { get; set; }
	public string? Reasons { get; set; }

	public virtual Student Student { get; set; } = default!;
	public virtual Schedule Schedule { get; set; } = default!;
}
