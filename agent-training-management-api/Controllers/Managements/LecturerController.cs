using AtmAPI.Models.DTOs.Lecturer;

namespace AtmAPI.Controllers.Managements;

public class LecturerController : ManagementControllerBase<Lecturer>
{
	private readonly IdentityService _identityService;

	public LecturerController(
		IdentityService identityService,
		ISieveProcessor sieveProc,
		AtmContext context,
		IMapper mapper,
		IUnitOfWork uow
	) : base(sieveProc, context, mapper, uow) => _identityService = identityService;

	/// <summary>
	/// 	Get Lecturers
	/// </summary>
	[HttpGet]
	public async Task<IActionResult> Get([FromQuery] SieveModel request) =>
		await HandleGetAsync<Lecturer, LecturerResponse>(request);

	/// <summary>
	/// 	Get unassigned lecturer by class id
	/// </summary>
	[HttpGet("classes/unassigned/{classId}")]
	public async Task<IActionResult> GetUnassignedLecturer(
		[FromQuery] SieveModel request,
		int classId
	) =>
		await HandleGetAsync<Lecturer, LecturerResponse>(
			request,
			includeProperties: "ClassLecturers",
			predicate: _ =>
				_.ClassLecturers == null
				|| _.ClassLecturers.Count == 0
				|| !_.ClassLecturers.Any(cl => cl.ClassId == classId)
		);

	/// <summary>
	/// 	Get assigned lecturer by class id
	/// </summary>
	[HttpGet("classes/assigned/{classId}")]
	public async Task<IActionResult> GetAssignedLecturer(
		[FromQuery] SieveModel request,
		int classId
	) =>
		await HandleGetAsync<Lecturer, LecturerResponse>(
			request,
			includeProperties: "ClassLecturers",
			predicate: _ =>
				_.ClassLecturers != null
				&& _.ClassLecturers.Count != 0
				&& _.ClassLecturers.Any(cl => cl.ClassId == classId)
		);

	/// <summary>
	/// 	Get a lecturer by id
	/// </summary>
	[HttpGet("{id:int}")]
	public async Task<IActionResult> Get(int id)
	{
		var lecturer = await Repo.GetByIdAsync(id);
		lecturer.ThrowIfNullHttpStatus();
		return Ok(Mapper.Map<LecturerResponse>(lecturer));
	}

	/// <summary>
	/// 	Get schedules by lecturer id
	/// </summary>
	[HttpGet("{id:int}/schedules")]
	public IActionResult GetLecturerSchedules(int id)
	{
		var lecturerClassIds = Uow.ClassLecturer
			.GetQueryable(classLec => classLec.LecturerId == id && classLec.Class.Available == true)
			.Select(classLec => classLec.ClassId);
		lecturerClassIds
			.ThrowIfNullHttpStatus("Lecturer' schedule not found", HttpStatusCode.NotFound)
			.IfEmpty();

		if (lecturerClassIds.Any())
		{
			var lecturerSchedule = Uow.Schedule.GetQueryable(
				sched => lecturerClassIds.Contains(sched.ClassId),
				"Slot,Class"
			);
			return Ok(Mapper.Map<List<ScheduleResponse>>(lecturerSchedule));
		}

		return Ok();
	}

	/// <summary>
	/// 	Get attend schedules by lecturer id
	/// </summary>
	[HttpGet("{id}/attendances")]
	public async Task<IActionResult> GetAttendances(
		[FromQuery] SieveModel sieveRequest,
		[FromQuery] RangeRequest rangeRequest,
		int id
	)
	{
		var lecturerAttendances = await Repo.GetAttendancesByLecturerAync(id, rangeRequest, Mapper);
		var sieveAttendances = SieveProc.Apply(sieveRequest, lecturerAttendances.AsQueryable());

		return Ok(
			ResponseSieve.With(
				totalCount: lecturerAttendances.Count(),
				list: sieveAttendances,
				model: sieveRequest
			)
		);
	}

	/// <summary>
	/// 	Update a lecturer
	/// </summary>
	[HttpPut("{id:int}")]
	public async Task<IActionResult> Put(int id, [FromBody] LecturerUpdateRequest request)
	{
		var lecturerToUpdate = await Repo.GetByIdAsync(id);
		lecturerToUpdate.ThrowIfNullHttpStatus();

		var restoreEntity = new LecturerUpdateRequest { Email = lecturerToUpdate.Email, };

		Mapper.Map(request, lecturerToUpdate);

		if (string.IsNullOrEmpty(lecturerToUpdate.IdentityReference))
		{
			await Repo.UpdateAsync(lecturerToUpdate);
			return Ok();
		}

		var identityResponse = await _identityService.UpdateUserAsync(
			int.Parse(lecturerToUpdate.IdentityReference),
			Mapper.Map<UserUpsertRequest>(request)
		);

		try
		{
			await Repo.UpdateAsync(lecturerToUpdate);
			return Ok();
		}
		catch (Exception ex)
		{
			await _identityService.UpdateUserAsync(
				int.Parse(lecturerToUpdate.IdentityReference),
				Mapper.Map<UserUpsertRequest>(restoreEntity)
			);

			throw new Exception(
				$"Update lecturer with id {id} failed, trying to revert on IDS with user ID:{lecturerToUpdate.IdentityReference}",
				innerException: ex
			);
		}
	}

	/// <summary>
	/// 	Add a lecturer
	/// </summary>
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] LecturerInsertRequest request)
	{
		var identityResponse = await _identityService.CreateUserAsync(
			Mapper.Map<UserUpsertRequest>(request),
			ModelConstants.Lecturer
		);

		try
		{
			var lecturer = Mapper.Map<Lecturer>(request);
			lecturer.IdentityReference = ((int)identityResponse.Content?.id).ToString();
			var result = await Repo.InsertAsync(lecturer);
			return Ok(Mapper.Map<LecturerResponse>(result));
		}
		catch (Exception ex)
		{
			await _identityService.DeleteUserAsync((int)identityResponse.Content?.id);
			throw new Exception(
				$"Create lecturer failed, trying to revert on IDS with user ID:{identityResponse.Content?.id}",
				innerException: ex
			);
		}
	}

	/// <summary>
	/// 	Delete a lecturer
	/// </summary>
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var lecturerToDelete = await Repo.GetByIdAsync(id);
		lecturerToDelete.ThrowIfNullHttpStatus(status: HttpStatusCode.OK);

		if (string.IsNullOrEmpty(lecturerToDelete.IdentityReference))
		{
			await Repo.DeleteAsync(lecturerToDelete);
			return Ok();
		}

		using (var trans = await Repo.BeginTransactionAsync())
		{
			var syncTask = await Repo.DeleteAsync(lecturerToDelete)
				.ContinueWith(t =>
				{
					return t.Exception != null
						? throw t.Exception
						: _identityService.DeleteUserAsync(
							int.Parse(lecturerToDelete.IdentityReference!)
						);
				});
			//todo: test
			try
			{
				await syncTask;
			}
			catch (Exception ex)
			{
				await trans.RollbackAsync();
				throw new Exception(
					$"Delete user ID:{lecturerToDelete.IdentityReference} failed, trying to revert on ATM with lecturer ID:{id}",
					innerException: ex
				);
			}

			await trans.CommitAsync();
		}

		return Ok();
	}
}
