namespace AtmAPI.Controllers.Managements;

public class ScheduleController : ManagementControllerBase<Schedule>
{
	public ScheduleController(
		ISieveProcessor sieveProc,
		AtmContext context,
		IMapper mapper,
		IUnitOfWork uow
	) : base(sieveProc, context, mapper, uow) { }

	/// <summary>
	/// 	Get schedules
	/// </summary>
	[HttpGet]
	public async Task<IActionResult> Get([FromQuery] SieveModel request) =>
		await HandleGetAsync<Schedule, ScheduleResponse>(request, "Slot, Class");

	/// <summary>
	/// 	Get a schedule by id
	/// </summary>
	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetById(int id)
	{
		var schedule = (await Repo.GetByIdAsync(id, "Slot,Class")).ThrowIfNullHttpStatus().Value;
		return Ok(Mapper.Map<ScheduleResponse>(schedule));
	}

	/// <summary>
	/// 	Update a schedule
	/// </summary>
	[HttpPut("{id:int}")]
	public async Task<IActionResult> Put([FromBody] ScheduleUpsertRequest request, int id)
	{
		var scheduleToUpdate = await Repo.GetByIdAsync(id);
		scheduleToUpdate.ThrowIfNullHttpStatus();

		Mapper.Map(request, scheduleToUpdate);
		await Repo.UpdateAsync(scheduleToUpdate);

		return Ok();
	}

	/// <summary>
	/// 	Delete a schedule
	/// </summary>
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		await Repo.DeleteByIdAsync(id);
		return Ok();
	}

	/// <summary>
	/// 	Get attendances by schedule id
	/// </summary>
	[HttpGet("{id:int}/attendances")]
	public async Task<IActionResult> GetAttendances(
		[FromQuery] SieveModel sieveRequest,
		[FromQuery] RangeRequest rangeRequest,
		int id
	)
	{
		var studentAttendances = await Repo.GetAttendancesByScheduleAsync(id, rangeRequest, Mapper);
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
	/// 	Checks schedule attendances
	/// </summary>
	[HttpPatch("{id:int}/attendances")]
	public async Task<IActionResult> ChecksAttendances(
		[FromBody] IEnumerable<int>? absenteeIdsToAdd,
		[FromQuery] DateTime? attendDate,
		int id
	)
	{
		var scheduleDate = attendDate ?? DateTime.Today;
		var schedule = await Repo.GetByIdAsync(id);
		schedule.ThrowIfNullHttpStatus("Schedule not found", HttpStatusCode.NotFound);

		// check class schedule attended
		await schedule.ScheduleCheckIns
			.SingleOrDefault(sc => sc.CheckInDate == scheduleDate && sc.ScheduleId == schedule.Id)
			.CallbackAsync(async value =>
			{
				if (value != null)
					return;

				schedule.ScheduleCheckIns.Add(new(scheduleDate, schedule.Id));
				await Uow.Schedule.UpdateAsync(schedule);
			});

		if (!absenteeIdsToAdd.IsAny())
			return Ok();

		var existsAbsences = Uow.Absence.GetQueryable(
			abs => abs.ScheduleId == schedule.Id && abs.AbsenceDate.Date == scheduleDate.Date
		);

		if (existsAbsences.IsAny())
		{
			await existsAbsences
				.Where(existsAbs => !absenteeIdsToAdd.Contains(existsAbs.Id))
				.CallbackAsync(async value =>
				{
					if (value != null)
						await Uow.Absence.DeleteRangeAsync(value);
				});

			await absenteeIdsToAdd
				.Where(studId => !existsAbsences.Select(ea => ea.Id).Contains(studId))
				.Select(studId => new Absence(schedule.Id, studId, scheduleDate))
				.CallbackAsync(async value =>
				{
					if (value != null)
						await Uow.Absence.InsertRangeAsync(value);
				});
		}
		else
		{
			await absenteeIdsToAdd
				.Select(studId => new Absence(schedule.Id, studId, scheduleDate))
				.CallbackAsync(async value =>
				{
					if (value != null)
						await Uow.Absence.InsertRangeAsync(value);
				});
		}

		return Ok();
	}
}
