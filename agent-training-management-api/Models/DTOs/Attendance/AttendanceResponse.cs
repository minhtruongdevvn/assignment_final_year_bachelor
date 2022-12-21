namespace AtmAPI.Models.DTOs.Attendance;

public class AttendanceResponse
{
	public AttendanceResponse() { }

	public AttendanceResponse(
		int scheduleId,
		bool? isAttended,
		DateTime attendDate,
		SlotResponse? slot,
		ClassResponse? @class,
		StudentResponse? student = null,
		string? absenceReasons = null
	)
	{
		IsAttended = isAttended;
		AttendDate = attendDate;
		Slot = slot;
		@Class = @class;
		AbsenceReasons = absenceReasons;
		Student = student;
		ScheduleId = scheduleId;
	}

	public int ScheduleId { get; set; }
	public bool? IsAttended { get; set; }
	public DateTime? AttendDate { get; set; }
	public string? AbsenceReasons { get; set; }
	public SlotResponse? Slot { get; set; }
	public ClassResponse? @Class { get; set; }
	public StudentResponse? Student { get; set; }
}
