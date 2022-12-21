namespace AtmAPI.Controllers.Managements;

public class SlotController : ManagementControllerBase<Slot>
{
	public SlotController(
		ISieveProcessor sieveProc,
		AtmContext context,
		IMapper mapper,
		IUnitOfWork uow
	) : base(sieveProc, context, mapper, uow) { }

	/// <summary>
	/// 	Get slots
	/// </summary>
	[HttpGet]
	public async Task<IActionResult> Get([FromQuery] SieveModel request) =>
		await HandleGetAsync<Slot, SlotResponse>(request);

	/// <summary>
	/// 	Get unassigned slots by class id
	/// </summary>
	[HttpGet("classes/unassigned/{classId}")]
	public async Task<IActionResult> GetUnassignedSlotByClassId(
		[FromQuery] SieveModel request,
		int classId
	) =>
		await HandleGetAsync<Slot, SlotResponse>(
			request,
			includeProperties: "Schedules",
			predicate: _ =>
				_.Schedules == null
				|| _.Schedules.Count == 0
				|| !_.Schedules.Any(cl => cl.ClassId == classId)
		);

	/// <summary>
	/// 	Get assigned slots by class id
	/// </summary>
	[HttpGet("classes/assigned/{classId}")]
	public async Task<IActionResult> GetAssignedSlotByClassId(
		[FromQuery] SieveModel request,
		int classId
	) =>
		await HandleGetAsync<Slot, SlotResponse>(
			request,
			includeProperties: "Schedules",
			predicate: _ =>
				_.Schedules != null
				&& _.Schedules.Count != 0
				&& _.Schedules.Any(cl => cl.ClassId == classId)
		);

	/// <summary>
	/// 	Get a slot by id
	/// </summary>
	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetById(int id)
	{
		var slot = await Repo.GetByIdAsync(id);
		slot.ThrowIfNullHttpStatus();
		return Ok(Mapper.Map<SlotResponse>(slot));
	}

	/// <summary>
	/// 	Add a slot
	/// </summary>
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] SlotUpsertRequest request)
	{
		var result = await Repo.InsertAsync(Mapper.Map<Slot>(request));
		return Ok(Mapper.Map<SlotResponse>(result));
	}

	/// <summary>
	/// 	Update a slot
	/// </summary>
	[HttpPut("{id:int}")]
	public async Task<IActionResult> Put(int id, [FromBody] SlotUpsertRequest request)
	{
		var slotToUpdate = await Repo.GetByIdAsync(id);
		slotToUpdate.ThrowIfNullHttpStatus();

		Mapper.Map(request, slotToUpdate);
		await Repo.UpdateAsync(slotToUpdate);

		return Ok();
	}

	/// <summary>
	/// 	Delete a slot
	/// </summary>
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		await Repo.DeleteByIdAsync(id);
		return Ok();
	}
}
