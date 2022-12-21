namespace AtmAPI.Models.Entities;

public class Slot : EntityBase
{
	public DayOfWeek DayOfWeek { get; set; }
	public TimeSpan StartAt { get; set; }
	public TimeSpan EndAt { get; set; }

	public virtual ICollection<Schedule>? Schedules { get; set; }
}
