namespace AtmAPI.Models.DTOs.Attendance;

public class LecturerAttendanceResponse
{
	public LecturerAttendanceResponse() { }

	public LecturerAttendanceResponse(
		int scheduleId,
		bool? isAttended,
		DateTime attendDate,
		SlotResponse? slot,
		ClassResponse? @class
	)
	{
		IsAttended = isAttended;
		AttendDate = attendDate;
		Slot = slot;
		@Class = @class;
		ScheduleId = scheduleId;
	}

	public int ScheduleId { get; set; }
	public bool? IsAttended { get; set; }
	public DateTime? AttendDate { get; set; }
	public SlotResponse? Slot { get; set; }
	public ClassResponse? @Class { get; set; }
}
