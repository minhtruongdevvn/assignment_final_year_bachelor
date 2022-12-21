namespace AtmAPI.Models.DTOs.Attendance;

public class TimetableResponse
{
	public TimetableResponse() { }

	public TimetableResponse(
		bool? isCheckedIn,
		DateTime? checkInDate,
		SlotResponse? slot = null,
		ClassResponse? @class = null,
		SkillResponse? skill = null
	)
	{
		IsCheckedIn = isCheckedIn;
		CheckInDate = checkInDate;
		Slot = slot;
		Class = @class;
		Skill = skill;
	}

	public bool? IsCheckedIn { get; set; }
	public DateTime? CheckInDate { get; set; }
	public SlotResponse? Slot { get; set; }
	public ClassResponse? @Class { get; set; }
	public SkillResponse? Skill { get; set; }
}
