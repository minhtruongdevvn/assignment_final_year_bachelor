namespace AtmAPI.Models.DTOs.Schedule;

public class ScheduleUpsertRequest
{
	[Required]
	public int SlotId { get; set; }

	[Required]
	public int ClassId { get; set; }
}
