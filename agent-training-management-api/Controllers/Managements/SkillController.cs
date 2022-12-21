using AtmAPI.Models.DTOs.Skill;
using Hangfire;

namespace AtmAPI.Controllers.Managements;

public class SkillController : ManagementControllerBase<Skill>
{
	private readonly AssignmentService _assignmentService;

	public SkillController(
		ISieveProcessor sieveProc,
		AtmContext context,
		IMapper mapper,
		IUnitOfWork uow,
		AssignmentService assignmentService
	) : base(sieveProc, context, mapper, uow) => _assignmentService = assignmentService;

	/// <summary>
	/// 	Get skills
	/// </summary>
	[HttpGet]
	public async Task<IActionResult> Get([FromQuery] SieveModel request)
	{
		return await HandleGetAsync<Skill, SkillResponse>(
			includeProperties: "Category,Classes",
			repository: Repo,
			model: request
		);
	}

	/// <summary>
	/// 	Get all skills
	/// </summary>
	[HttpGet("all")]
	public async Task<IActionResult> GetAll()
		=> Ok(await Repo.EntitySet.ToListAsync());
	

	/// <summary>
	/// 	Get a skill by id
	/// </summary>
	[HttpGet("{id:int}")]
	public async Task<IActionResult> Get(int id)
	{
		var skill = await Repo.GetByIdAsync(id);
		return Ok(Mapper.Map<SkillResponse>(skill));
	}

	/// <summary>
	/// 	Update a skill
	/// </summary>
	[HttpPut("{id:int}")]
	public async Task<IActionResult> Put(int id, [FromBody] SkillUpsertRequest request)
	{
		var skillToUpdate = await Repo.GetByIdAsync(id);
		skillToUpdate.ThrowIfNullHttpStatus();

		var tmpName = skillToUpdate.Name;
		Mapper.Map(request, skillToUpdate);
		await Repo.UpdateAsync(skillToUpdate);

		// sync to assignment system
		BackgroundJob.Enqueue(
			() =>
				_assignmentService.UpdateSkillAsync(
					new()
					{
						Name = skillToUpdate.Name,
						OldName = tmpName,
						Description = skillToUpdate.Description,
					}
				)
		);

		return Ok();
	}

	/// <summary>
	/// 	Add a skill
	/// </summary>
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] SkillUpsertRequest request)
	{
		var result = await Repo.InsertAsync(Mapper.Map<Skill>(request));
		result.ThrowIfNullHttpStatus();

		// sync to assignment system
		BackgroundJob.Enqueue(
			() =>
				_assignmentService.CreateSkillAsync(
					new() { Name = result.Name, Description = result.Description, }
				)
		);

		return Ok(Mapper.Map<SkillResponse>(result));
	}

	/// <summary>
	/// 	Delete a skill
	/// </summary>
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var deleteSkill = await Repo.GetQueryable().SingleOrDefaultAsync(skill => skill.Id == id);
		deleteSkill.ThrowIfNullHttpStatus();
		await Repo.DeleteAsync(deleteSkill);

		// sync to assignment system
		BackgroundJob.Enqueue(() => _assignmentService.DeleteSkillAsync(deleteSkill.Name));

		return Ok();
	}
}
