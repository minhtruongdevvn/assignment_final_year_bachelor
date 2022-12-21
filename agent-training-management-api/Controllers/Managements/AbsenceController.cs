namespace AtmAPI.Controllers.Managements;

public class AbsenceController : ManagementControllerBase<Absence>
{
	public AbsenceController(
		ISieveProcessor sieveProc,
		AtmContext context,
		IMapper mapper,
		IUnitOfWork uow
	) : base(sieveProc, context, mapper, uow) { }

	/// <summary>
	/// 	Get an absence by id
	/// </summary>
	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetById(int id)
	{
		var absence = await Repo.GetByIdAsync(id);
		absence.ThrowIfNullHttpStatus();
		return Ok(Mapper.Map<AttendanceResponse>(absence));
	}

	/// <summary>
	/// 	Get absences
	/// </summary>
	[HttpGet]
	public async Task<IActionResult> GetAbsences([FromQuery] SieveModel request)
	{
		return await HandleGetAsync<Absence, AbsenceResponse>(
			includeProperties: "Schedule.Slot, Student",
			repository: Repo,
			model: request
		);
	}
}
