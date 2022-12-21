namespace AtmAPI.Extensions;

public static class RepositoryExtensions
{
	/// <summary>
	/// 	If the start or end of the slot to check is between the start and end of any
	/// 	of the active slots, then there is a conflict
	/// </summary>
	/// <remark><see cref="GenericRepository{Slot}"/></remark>
	public static bool AnyConflictedSlots(
		this GenericRepository<Slot> repo,
		Slot slotToCheck,
		IEnumerable<Slot> activeSlots
	)
	{
		_ = repo;

		return activeSlots
			.Where(activeSlot => activeSlot.DayOfWeek == slotToCheck.DayOfWeek)
			.Any(matchConflicts);

		bool matchConflicts(Slot activeSlot) =>
			!(slotToCheck.EndAt < activeSlot.StartAt || slotToCheck.StartAt > activeSlot.EndAt);
	}

	public static async Task<IEnumerable<AttendanceResponse>> GetAttendancesByStudentAsync(
		this GenericRepository<Student> repo,
		int id,
		RangeRequest range,
		IMapper mapper
	)
	{
		var student = repo.GetQueryable(
			stud => stud.IdentityReference == id.ToString(),
			"SkillReports"
		);
		student.ThrowIfNullHttpStatus("Student not found", HttpStatusCode.NotFound).IfEmpty();

		var reports = student
			.Where(stud => stud.SkillReports != null)
			.SelectMany(stud => stud.SkillReports!)
			.IncludesSplitQuery("Student, Class.Schedules, Class.Skill");
		reports
			.ThrowIfNullHttpStatus("Student skill reports not found", HttpStatusCode.NotFound)
			.IfEmpty();

		var schedules = await reports
			.Where(report => report.Class.Schedules != null)
			.SelectMany(report => report.Class.Schedules!)
			.IncludesSplitQuery("Absences, ScheduleCheckIns, Slot")
			.ToListAsync();
		schedules
			.ThrowIfNullHttpStatus("Student class schedules not found", HttpStatusCode.NotFound)
			.IfEmpty();

		return ProcessStudentsAttendances(schedules, await reports.ToListAsync(), range, mapper);
	}

	public static async Task<AttendanceResponse> GetAttendanceByKeysAsync(
		this GenericRepository<Student> repo,
		int id,
		int scheduleId,
		DateTime attendDate,
		IMapper mapper
	)
	{
		var student = repo.GetQueryable(stud => stud.Id == id, "SkillReports");
		student.ThrowIfNullHttpStatus("Student not found", HttpStatusCode.NotFound).IfEmpty();

		var reports = student
			.Where(stud => stud.SkillReports != null)
			.SelectMany(stud => stud.SkillReports!)
			.IncludesSplitQuery("Student, Class.Skill.Category, Class.ClassLecturers.Lecturer");
		reports
			.ThrowIfNullHttpStatus("Student skill reports not found", HttpStatusCode.NotFound)
			.IfEmpty();

		var schedules = await reports
			.Where(report => report.Class.Schedules != null)
			.SelectMany(report => report.Class.Schedules!)
			.IncludesSplitQuery("Absences, ScheduleCheckIns, Slot")
			.ToListAsync();
		schedules
			.ThrowIfNullHttpStatus("Student class schedules not found", HttpStatusCode.NotFound)
			.IfEmpty();

		var dateRange = new RangeRequest(attendDate, attendDate);
		var currentActiveAttendances = ProcessStudentsAttendances(
			schedules,
			await reports.ToListAsync(),
			dateRange,
			mapper
		);

		return currentActiveAttendances.First(aa => aa.ScheduleId == scheduleId);
	}

	public static async Task<IEnumerable<AttendanceResponse>> GetAttendancesByScheduleAsync(
		this GenericRepository<Schedule> repo,
		int id,
		RangeRequest range,
		IMapper mapper
	)
	{
		var schedules = repo.GetQueryable(
			sched => sched.Id == id,
			"Absences, ScheduleCheckIns, Slot, Class.SkillReports, Skill"
		);
		schedules
			.ThrowIfNullHttpStatus("Class schedules not found", HttpStatusCode.NotFound)
			.IfEmpty();

		var reports = await schedules
			.Select(sched => sched.Class)
			.Where(c => c.SkillReports != null)
			.SelectMany(c => c.SkillReports!)
			.IncludesSplitQuery("Student")
			.ToListAsync();
		reports
			.ThrowIfNullHttpStatus("Class students not found", HttpStatusCode.NotFound)
			.IfEmpty();

		return ProcessStudentsAttendances(await schedules.ToListAsync(), reports, range, mapper);
	}

	public static async Task<IEnumerable<AttendanceResponse>> GetAttendancesByClassAsync(
		this GenericRepository<Class> repo,
		int id,
		RangeRequest range,
		IMapper mapper
	)
	{
		var @class = repo.GetQueryable(c => c.Id == id, "Schedules, SkillReports, Skill");
		@class.ThrowIfNullHttpStatus("Class not found", HttpStatusCode.NotFound).IfEmpty();

		var classSchedules = await @class
			.Where(c => c.Schedules != null)
			.SelectMany(c => c.Schedules!)
			.IncludesSplitQuery("Absences, ScheduleCheckIns, Slot")
			.ToListAsync();
		classSchedules
			.ThrowIfNullHttpStatus("Class schedules not found", HttpStatusCode.NotFound)
			.IfEmpty();

		var classSkillReports = await @class
			.Where(c => c.SkillReports != null)
			.SelectMany(c => c.SkillReports!)
			.IncludesSplitQuery("Student")
			.ToListAsync();
		classSkillReports
			.ThrowIfNullHttpStatus("Class students not found", HttpStatusCode.NotFound)
			.IfEmpty();

		return ProcessStudentsAttendances(classSchedules, classSkillReports, range, mapper);
	}

	public static async Task<IEnumerable<TimetableResponse>> GetTimetableByClassIdAsync(
		this GenericRepository<Class> repo,
		int id,
		RangeRequest range,
		IMapper mapper
	)
	{
		var classQuery = repo.GetQueryable(c => c.Id == id, "Schedules, SkillReports, Skill");
		classQuery.ThrowIfNullHttpStatus("Class not found", HttpStatusCode.NotFound).IfEmpty();

		var classSchedules = await classQuery
			.Where(c => c.Schedules != null)
			.SelectMany(c => c.Schedules!)
			.IncludesSplitQuery("Absences, ScheduleCheckIns, Slot")
			.ToListAsync();
		classSchedules
			.ThrowIfNullHttpStatus("Class schedules not found", HttpStatusCode.NotFound)
			.IfEmpty();

		var classSkillReports = await classQuery
			.Where(c => c.SkillReports != null)
			.SelectMany(c => c.SkillReports!)
			.IncludesSplitQuery("Student")
			.ToListAsync();
		classSkillReports
			.ThrowIfNullHttpStatus("Class students not found", HttpStatusCode.NotFound)
			.IfEmpty();

		var @class = (await classQuery.FirstOrDefaultAsync()).ThrowIfNull().Value;

		return ProcessClassTimetable(classSchedules, @class, range, mapper);
	}

	public static async Task<IEnumerable<LecturerAttendanceResponse>> GetAttendancesByLecturerAync(
		this GenericRepository<Lecturer> repo,
		int id,
		RangeRequest range,
		IMapper mapper
	)
	{
		var lecturer = repo.GetQueryable(
			lec => lec.IdentityReference == id.ToString(),
			"ClassLecturers"
		);
		lecturer.ThrowIfNullHttpStatus("Lecturer not found", HttpStatusCode.NotFound).IfEmpty();

		var classLecturers = lecturer
			.Where(lec => lec.ClassLecturers != null)
			.SelectMany(lec => lec.ClassLecturers!)
			.IncludesSplitQuery("Lecturer, Class.Schedules, Class.Skill");
		classLecturers
			.ThrowIfNullHttpStatus("Lecturer not assigned to any classes.", HttpStatusCode.NotFound)
			.IfEmpty();

		var schedules = await classLecturers
			.Where(classLec => classLec.Class.Schedules != null)
			.SelectMany(classLec => classLec.Class.Schedules!)
			.IncludesSplitQuery("ScheduleCheckIns, Slot")
			.ToListAsync();
		schedules
			.ThrowIfNullHttpStatus("Lecturer class schedules not found", HttpStatusCode.NotFound)
			.IfEmpty();

		return ProcessLecturerAttendances(
			schedules,
			await classLecturers.ToListAsync(),
			range,
			mapper
		);
	}

	/// <summary>
	/// 	Return a list of <see cref="AttendanceResponse"/> by iterate through
	/// 	all attendance dates, checks attendance for each student.
	/// </summary>
	private static IEnumerable<AttendanceResponse> ProcessStudentsAttendances(
		IEnumerable<Schedule> schedules,
		IEnumerable<SkillReport> reports,
		RangeRequest range,
		IMapper mapper
	)
	{
		var studentAttendances = new List<AttendanceResponse>();
		var actualDates = GetActualAttendDateSchedules(schedules, range);

		foreach (var attendSched in actualDates)
		{
			// the date of class schedule is "perform teaching?" (or checked in),
			// null when the lecturer not performed "checking students attendances".
			var schedCheckIn = schedules
				.SelectMany(sched => sched.ScheduleCheckIns ?? new())
				.SingleOrDefault(sci => sci.CheckInDate.Date == attendSched.ActualDate.Date);

			foreach (var report in reports)
			{
				var classRes =
					report?.Class != null ? mapper.Map<ClassResponse>(report?.Class) : null;
				var absence = schedules
					.Where(sched => sched.Absences != null)
					.SelectMany(sched => sched.Absences!)
					.SingleOrDefault(
						abs =>
							abs.AbsenceDate == attendSched.ActualDate
							&& abs.StudentId == report?.StudentId
					);

				studentAttendances.Add(
					new(
						isAttended: schedCheckIn?.CheckInDate != null ? absence == null : null,
						slot: mapper.Map<SlotResponse>(attendSched.Schedule.Slot),
						student: mapper.Map<StudentResponse>(report?.Student),
						scheduleId: attendSched.Schedule.Id,
						attendDate: attendSched.ActualDate,
						absenceReasons: absence?.Reasons,
						@class: classRes
					)
				);
			}
		}

		return studentAttendances;
	}

	private static IEnumerable<LecturerAttendanceResponse> ProcessLecturerAttendances(
		IEnumerable<Schedule> schedules,
		IEnumerable<ClassLecturer> classLecturers,
		RangeRequest range,
		IMapper mapper
	)
	{
		var attendances = new List<LecturerAttendanceResponse>();
		var actualDates = GetActualAttendDateSchedules(schedules, range);

		foreach (var attendSched in actualDates)
		{
			var schedCheckIn = schedules
				.SelectMany(sched => sched.ScheduleCheckIns ?? new())
				.SingleOrDefault(sci => sci.CheckInDate.Date == attendSched.ActualDate.Date);

			foreach (var classLec in classLecturers)
			{
				var classRes =
					classLec.Class != null ? mapper.Map<ClassResponse>(classLec.Class) : null;

				attendances.Add(
					new(
						isAttended: schedCheckIn?.CheckInDate != null ? true : null,
						slot: mapper.Map<SlotResponse>(attendSched.Schedule.Slot),
						scheduleId: attendSched.Schedule.Id,
						attendDate: attendSched.ActualDate,
						@class: classRes
					)
				);
			}
		}

		return attendances;
	}

	private static IEnumerable<TimetableResponse> ProcessClassTimetable(
		IEnumerable<Schedule> schedules,
		Class @class,
		RangeRequest range,
		IMapper mapper
	)
	{
		var timetable = new List<TimetableResponse>();
		var actualDates = GetActualAttendDateSchedules(schedules, range);

		foreach (var date in actualDates)
		{
			var schedCheckIn = schedules
				.SelectMany(sched => sched.ScheduleCheckIns ?? new())
				.SingleOrDefault(sci => sci.CheckInDate.Date == date.ActualDate.Date);

			timetable.Add(
				new(
					isCheckedIn: schedCheckIn?.CheckInDate != null,
					checkInDate: date.ActualDate,
					slot: mapper.Map<SlotResponse>(date.Schedule.Slot),
					@class: @class != null ? mapper.Map<ClassResponse>(@class) : null
				)
			);
		}

		return timetable;
	}

	// Get all possible attenable attendance schedule dates
	// within specified range.
	private static IEnumerable<(Schedule Schedule, DateTime ActualDate)> GetActualAttendDateSchedules(
		IEnumerable<Schedule> schedules,
		RangeRequest range
	)
	{
		var classStartDate = schedules.Min(sched => sched.Class.StartDate);
		var classEndDate = schedules.Max(sched => sched.Class.EndDate);

		var listOfActualDates = new List<(Schedule Schedule, DateTime ActualDate)>();
		var marker = range.From < classStartDate ? classStartDate : range.From;
		var endRange = range.To > classEndDate ? classEndDate : range.To;

		while (marker <= endRange)
		{
			var dateToCheck = marker;

			marker = marker.GetNextDay(DayOfWeek.Monday);

			if (dateToCheck.DayOfWeek == 0)
				continue;

			foreach (var sched in schedules)
			{
				if ((int)dateToCheck.DayOfWeek + (int)sched.Slot.DayOfWeek > 7) // 7 is max from Mon. to Sat.
					continue;

				var @class = sched.Class;
				var actualDate = dateToCheck.GetNextDay(sched.Slot.DayOfWeek, true).Date;

				if (actualDate >= @class.StartDate.Date && actualDate <= endRange)
					listOfActualDates.Add(new(sched, actualDate));
			}
		}

		return listOfActualDates;
	}
}
