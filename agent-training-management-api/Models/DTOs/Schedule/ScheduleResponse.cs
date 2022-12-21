namespace AtmAPI.Models.DTOs.Schedule;

public class ScheduleResponse : ResponseBase
{
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public ClassResponse? Class { get; set; }
	public SlotResponse? Slot { get; set; }
}
