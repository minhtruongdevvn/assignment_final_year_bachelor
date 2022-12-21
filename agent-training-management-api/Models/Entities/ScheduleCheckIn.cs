namespace AtmAPI.Models.Entities;

public class ScheduleCheckIn : EntityMetadataBase
{
	public ScheduleCheckIn() { }

	public ScheduleCheckIn(DateTime checkInDate, int scheduleId)
	{
		ScheduleId = scheduleId;
		CheckInDate = checkInDate.Date;
	}

	public ScheduleCheckIn(DateTime checkInDate, Schedule schedule)
	{
		ScheduleId = schedule.Id;
		CheckInDate = checkInDate.Date;
	}

	public int ScheduleId { get; set; }
	public DateTime CheckInDate { get; set; }

	public virtual Schedule Schedule { get; set; } = default!;
}
