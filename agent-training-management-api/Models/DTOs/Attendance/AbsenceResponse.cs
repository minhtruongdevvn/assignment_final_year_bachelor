namespace AtmAPI.Models.DTOs.Attendance;

public class AbsenceResponse : ResponseBase
{
	public DateTime? AbsenceDate { get; set; }
	public string? Reasons { get; set; }
	public SlotResponse? Slot { get; set; }
	public StudentResponse? Student { get; set; }
}
