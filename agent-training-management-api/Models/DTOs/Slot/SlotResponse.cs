namespace AtmAPI.Models.DTOs.Slot;

public class SlotResponse : ResponseBase
{
	public SlotResponse() { }

	public SlotResponse(DayOfWeek dayOfWeek, TimeSpan startAt, TimeSpan endAt)
	{
		DayOfWeek = dayOfWeek.ToString();
		StartAt = startAt;
		EndAt = endAt;
	}

	public string? DayOfWeek { get; set; }
	public TimeSpan StartAt { get; set; }
	public TimeSpan EndAt { get; set; }
	public IEnumerable<ScheduleResponse>? Schedules { get; set; }
}
