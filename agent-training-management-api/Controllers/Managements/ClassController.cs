namespace AtmAPI.Controllers.Managements;

[Route("classes")]
public class ClassController : ManagementControllerBase<Class>
{
	public ClassController(
		ISieveProcessor sieveProc,
		AtmContext context,
		IMapper mapper,
		IUnitOfWork uow
	) : base(sieveProc, context, mapper, uow) { }

	/// <summary>
	/// 	Get classes
	/// </summary>
	[HttpGet]
	public async Task<IActionResult> Get([FromQuery] SieveModel request) =>
		await HandleGetAsync<Class, ClassResponse>(
			request,
			"Skill, ClassLecturers.Lecturer",
			predicate: _ => !_.IsExternal
		);

	/// <summary>
	/// 	Get classes
	/// </summary>
	[HttpGet("lecturers/{lecturerId}")]
	public async Task<IActionResult> GetClassByLecturer(
		[FromQuery] SieveModel request,
		int lecturerId
	) =>
		await HandleGetAsync<Class, ClassResponse>(
			request,
			"Skill, ClassLecturers.Lecturer",
			predicate: _ =>
				!_.IsExternal
				&& _.ClassLecturers != null
				&& _.ClassLecturers.Any(
					cl => cl.Lecturer.IdentityReference == lecturerId.ToString()
				)
		);

	/// <summary>
	/// 	Get a class by id
	/// </summary>
	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetById(int id)
	{
		var @class = await Repo.GetByIdAsync(id, "Skill,ClassLecturers.Lecturer");
		@class.ThrowIfNullHttpStatus();
		return Ok(Mapper.Map<ClassResponse>(@class));
	}

	/// <summary>
	/// 	Update a class
	/// </summary>
	[HttpPut("{id:int}")]
	public async Task<IActionResult> Put(int id, [FromBody] ClassUpsertRequest request)
	{
		var classToUpdate = await Repo.GetByIdAsync(id);
		classToUpdate.ThrowIfNullHttpStatus();

		Mapper.Map(request, classToUpdate);
		await Repo.UpdateAsync(classToUpdate);

		return Ok();
	}

	/// <summary>
	/// 	Add a class
	/// </summary>
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] ClassUpsertRequest request)
	{
		var createdClass = await Repo.InsertAsync(Mapper.Map<Class>(request));
		return Ok(Mapper.Map<ClassResponse>(createdClass));
	}

	/// <summary>
	/// 	Delete a class
	/// </summary>
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		await Repo.DeleteByIdAsync(id);
		return Ok();
	}

	/// <summary>
	/// 	Get attendances by class id with specify range
	/// </summary>
	[HttpGet("{id:int}/attendances")]
	public async Task<IActionResult> GetAttendances(
		[FromQuery] SieveModel sieveRequest,
		[FromQuery] RangeRequest rangeRequest,
		int id
	)
	{
		var studentAttendances = await Repo.GetAttendancesByClassAsync(id, rangeRequest, Mapper);
		var sieveAttendances = SieveProc.Apply(sieveRequest, studentAttendances.AsQueryable());

		return Ok(
			ResponseSieve.With(
				totalCount: studentAttendances.Count(),
				list: sieveAttendances,
				model: sieveRequest
			)
		);
	}

	/// <summary>
	/// 	Get timetable by class id with specify range
	/// </summary>
	[HttpGet("{id:int}/timetables")]
	public async Task<IActionResult> GetClassTimetable(
		[FromQuery] SieveModel sieveRequest,
		[FromQuery] RangeRequest rangeRequest,
		int id
	)
	{
		var timetables = await Repo.GetTimetableByClassIdAsync(id, rangeRequest, Mapper);
		var sieveTimetable = SieveProc.Apply(sieveRequest, timetables.AsQueryable());

		return Ok(
			ResponseSieve.With(
				totalCount: timetables.Count(),
				list: sieveTimetable,
				model: sieveRequest
			)
		);
	}

	/// <summary>
	/// 	Get schedules by class id
	/// </summary>
	[HttpGet("{id:int}/schedules")]
	public async Task<IActionResult> GetSchedulesByClass([FromQuery] SieveModel request, int id)
	{
		request.Filters += $",class_id=={id}";
		return await HandleGetAsync<Schedule, ScheduleResponse>(request, "Slot");
	}

	/// <summary>
	/// 	Add schedules to class
	/// </summary>
	[HttpPost("{id:int}/slots")]
	public async Task<IActionResult> AddSlotsToClass(
		[FromBody] IEnumerable<int> slotIdsToAdd,
		int id
	)
	{
		var slotsToAdd = await Uow.Slot
			.GetQueryable(slot => slotIdsToAdd.Contains(slot.Id))
			.ToListAsync();
		slotsToAdd.ThrowIfNullHttpStatus("Slots not found", HttpStatusCode.NotFound).IfEmpty();

		var classSlots = Uow.Schedule
			.GetQueryable(sched => sched.ClassId == id, "Slot")
			?.Select(sched => sched.Slot);

		if (classSlots != null)
		{
			var conflictedSlots = new { Selfs = new List<Slot>(), Others = new List<Slot>() };
			foreach (var slot in slotsToAdd)
			{
				if (Uow.Slot.AnyConflictedSlots(slot, slotsToAdd.Where(slot => slot.Id != slot.Id)))
					conflictedSlots.Selfs.Add(slot);
				if (Uow.Slot.AnyConflictedSlots(slot, classSlots))
					conflictedSlots.Others.Add(slot);
			}

			conflictedSlots
				.ThrowHttpStatus(
					new
					{
						Message = "One or more slot has conflict with others",
						SelfConflicts = conflictedSlots.Selfs,
						Conflicts = conflictedSlots.Others
					}
				)
				.IfTrue(c => c.Selfs.Any() || c.Others.Any());
		}

		await Uow.Schedule.InsertRangeAsync(
			slotIdsToAdd.Select(slotId => new Schedule { ClassId = id, SlotId = slotId })
		);

		var addedSlotIds = slotsToAdd.Select(slot => slot.Id);
		return Ok(
			new
			{
				AddedSlotIds = addedSlotIds,
				ErrorSlotIds = slotIdsToAdd.Where(slotId => !addedSlotIds.Contains(slotId))
			}
		);
	}

	/// <summary>
	/// 	Delete slots from a class
	/// </summary>
	[HttpPost("{id:int}/slots/delete")]
	public async Task<IActionResult> DeleteSlotsFromClass(
		[FromBody] IEnumerable<int> slotIdsToDelete,
		int id
	)
	{
		slotIdsToDelete.ThrowHttpStatus().IfEmpty();
		await HandleDeleteAsync<Schedule>(
			sched => sched.ClassId == id && slotIdsToDelete.Contains(sched.SlotId)
		);
		return Ok();
	}

	/// <summary>
	/// 	Get lecturers by class id
	/// </summary>
	[HttpGet("{id:int}/lecturers")]
	public async Task<IActionResult> GetLecturersByClass([FromQuery] SieveModel request, int id)
	{
		request.Filters += $",class_id=={id}";
		return await HandleGetAsync<ClassLecturer, ClassLecturerResponse>(request, "Lecturer");
	}

	/// <summary>
	/// 	Add lecturers to a class
	/// </summary>
	[HttpPost("{id:int}/lecturers")]
	public async Task<IActionResult> AddLecturersToClass(
		[FromBody] IEnumerable<int> lecturerIdsToAdd,
		int id
	)
	{
		(await Repo.GetByIdAsync(id)).ThrowIfNullHttpStatus();

		var lecturersToAdd = await Uow.Lecturer
			.GetQueryable(
				lec =>
					lecturerIdsToAdd.Contains(lec.Id)
					&& lec.ClassLecturers != null
					&& !lec.ClassLecturers.Any(cl => cl.ClassId == id)
			)
			.ToListAsync();
		lecturersToAdd
			.ThrowIfNullHttpStatus("Lecturers not found", HttpStatusCode.NotFound)
			.IfEmpty();

		await lecturersToAdd
			.Select(lec =>
			{
				var userClasses = lecturersToAdd
					.Where(lec => lec.ClassLecturers != null)
					.SelectMany(lec => lec.ClassLecturers!)
					.Select(cl => cl.Class);
				return new UsersActiveClasses<Lecturer> { User = lec, Classes = userClasses };
			})
			.CallbackAsync(value => ValidateClassSlotsForConflictsAsync(value, id));

		await Uow.ClassLecturer.InsertRangeAsync(
			lecturerIdsToAdd.Select(lecId => new ClassLecturer { ClassId = id, LecturerId = lecId })
		);

		var addedLecurerIds = lecturersToAdd.Select(lec => lec.Id);
		return Ok(
			new
			{
				added = addedLecurerIds,
				alreadyAdded = lecturerIdsToAdd.Where(lecId => !addedLecurerIds.Contains(lecId))
			}
		);
	}

	/// <summary>
	/// 	Dlete lecturers from a class
	/// </summary>
	[HttpPost("{id:int}/lecturers/delete")]
	public async Task<IActionResult> DeleteLecturersFromClass(
		[FromBody] IEnumerable<int> lecturerIdsToDelete,
		int id
	)
	{
		await HandleDeleteAsync<ClassLecturer>(
			classLec => classLec.ClassId == id && lecturerIdsToDelete.Contains(classLec.LecturerId)
		);
		return Ok();
	}

	/// <summary>
	/// 	Get students by class id
	/// </summary>
	[HttpGet("{id:int}/students")]
	public async Task<IActionResult> GetStudentsByClass([FromQuery] SieveModel request, int id)
	{
		return await HandleGetAsync<Student, StudentResponse>(
			includeProperties: "SkillReports.Class",
			model: request,
			predicate: student =>
				student.SkillReports != null
				&& student.SkillReports.Any(report => report.ClassId == id)
		);
	}

	/// <summary>
	/// 	Add students to a class
	/// </summary>
	[HttpPost("{id:int}/students")]
	public async Task<IActionResult> AddStudentsToClass(
		[FromBody] IList<int> studentIdsToAdd,
		int id
	)
	{
		(await Repo.GetByIdAsync(id)).ThrowIfNullHttpStatus();

		// get students that are not in the class
		var studentsToAdd = await Uow.Student
			.GetQueryable(
				includeProperties: "SkillReports,SkillReports.Class",
				predicate: stud =>
					studentIdsToAdd.Contains(stud.Id)
					&& stud.SkillReports != null
					&& !stud.SkillReports.Any(r => r.ClassId == id)
			)
			.ToListAsync();
		studentsToAdd
			.ThrowIfNullHttpStatus("Students not found", HttpStatusCode.NotFound)
			.IfEmpty();

		// enrolled
		await Uow.SkillReport.InsertRangeAsync(
			studentsToAdd.Select(stud => new SkillReport { ClassId = id, StudentId = stud.Id, })
		);

		// SECTION - untested
		var addedStudentIds = studentsToAdd.Select(stud => stud.Id);
		return Ok(
			new
			{
				added = addedStudentIds,
				alreadyAdded = studentIdsToAdd.Where(studId => !addedStudentIds.Contains(studId))
			}
		);
		// !SECTION
	}

	/// <summary>
	/// 	Delete students from a class [DEPRECATED]
	/// </summary>
	[HttpDelete("{id:int}/students")]
	[Obsolete("Deprecated. Logic not in use yet.")]
	public async Task<IActionResult> DeleteStudentsFromClass(
		[FromBody] IEnumerable<int> studentIdsToDelete,
		int id
	)
	{
		foreach (var studentId in studentIdsToDelete)
		{
			var reportToDelete = await Uow.SkillReport.EntitySet.FirstOrDefaultAsync(
				report => report.StudentId == studentId && report.ClassId == id
			);
			reportToDelete.ThrowIfNullHttpStatus();
			await Uow.SkillReport.DeleteAsync(reportToDelete);
		}

		return Ok();
	}

	private async Task ValidateClassSlotsForConflictsAsync<TUser>(
		IEnumerable<UsersActiveClasses<TUser>>? usersClasses,
		int classIdToChecks
	) where TUser : IdentityBase
	{
		if (!usersClasses.IsAny())
			return;

		var slotsToCheck = await Uow.Schedule
			.GetQueryable(sched => sched.ClassId == classIdToChecks, "Slot")
			.Select(sched => sched.Slot)
			.ToListAsync();
		slotsToCheck
			.ThrowIfNullHttpStatus("Classes' schedules not found", HttpStatusCode.NotFound)
			.IfEmpty();

		// filtering null nuisance
		var activeClasses = usersClasses
			.Where(uc => uc.Classes.IsAny())
			.SelectMany(uc => uc.Classes!)
			.DistinctBy(c => c.Id)
			.ToList()
			.Select(c => c.Id);
		if (!activeClasses.IsAny())
			return;

		// load all schedules from `usersClasses` to reduce computation in next code
		var schedulesOfAllActiveClasses = await Uow.Schedule
			.GetQueryable(sched => activeClasses.Contains(sched.ClassId))
			.ToListAsync();
		schedulesOfAllActiveClasses
			.ThrowIfNullHttpStatus("Classes' schedule not found", HttpStatusCode.NotFound)
			.IfEmpty();

		var usersWithConflictedSlots =
			from uc in usersClasses
			let conflictedSlots = schedulesOfAllActiveClasses
				.Where(sched => uc.Classes!.Any(c => c.Id == sched.ClassId))
				.Select(sched => sched.Slot)
				.Where(userSlot => Uow.Slot.AnyConflictedSlots(userSlot, slotsToCheck))
			where conflictedSlots.Any()
			select new { uc.User, ConflictedSlots = conflictedSlots, };

		usersWithConflictedSlots
			.ThrowHttpStatus(
				new
				{
					Message = $"One or more added {typeof(TUser)}s has conflict schedule",
					Trace = usersWithConflictedSlots
				}
			)
			.IfNotEmpty();
	}

	private class UsersActiveClasses<TUser>
	{
		public TUser? User { get; set; }
		public IEnumerable<Class>? Classes { get; set; }
	}
}
