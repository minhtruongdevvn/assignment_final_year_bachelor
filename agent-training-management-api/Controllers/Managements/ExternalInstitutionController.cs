namespace AtmAPI.Controllers.Managements;

[Route("externals")]
public class ExternalInstitutionController : ManagementControllerBase<ExternalInstitution>
{
	public ExternalInstitutionController(
		ISieveProcessor sieveProc,
		AtmContext context,
		IMapper mapper,
		IUnitOfWork uow
	) : base(sieveProc, context, mapper, uow) { }

	/// <summary>
	/// 	Get externals
	/// </summary>
	[HttpGet]
	public async Task<IActionResult> Get([FromQuery] SieveModel request)
	{
		var list = await Repo.GetListAsync(request);
		var count = await Repo.CountAsync(request.Filters);
		var pagination = ResponseSieve.With<ExternalInstitution, ExternalInstitutionResponse>(request, list, count, Mapper);

		var skills = await Uow.Skill.EntitySet.ToListAsync();

		pagination.Data = pagination?.Data?.Select(_ =>
		{
			var skillIds = _.SkillIds.Split(';');

			_.SkillNames = string.Join(
				"; ",
				skillIds.Select((skillId) => skills
					.First(skill => skill.Id == int.Parse(skillId)).Name)
			);

			return _;
		});

		return Ok(pagination);
	}


	/// <summary>
	/// 	Add an external
	/// </summary>
	[HttpPost("{nameIdentify}/{skillIds}")]
	public async Task<IActionResult> Add(string nameIdentify, string skillIds)
	{
		static string getUniqueString() => Guid.NewGuid().ToString();

		var newExternal = new ExternalInstitution
		{
			Name = nameIdentify,
			Code = getUniqueString(),
			PassCode = getUniqueString() + getUniqueString(),
			SkillIds = skillIds
		};
		await Repo.InsertAsync(newExternal);

		var skillIdList = skillIds.Split(';');
		var addedClasses = skillIdList.Select(
			_ =>
				new Class
				{
					Name = nameIdentify,
					Placement = nameIdentify,
					StartDate = DateTime.UtcNow,
					Available = true,
					IsExternal = true,
					EnableAutomation = false,
					SkillId = int.Parse(_)
				}
		);

		await Uow.Class.InsertRangeAsync(addedClasses);

		return Ok(newExternal);
	}

	/// <summary>
	/// 	Get a external by id
	/// </summary>
	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		var external = await Repo.GetByIdAsync(id);
		external.ThrowIfNullHttpStatus();

		var externalResponse = Mapper.Map<ExternalInstitutionResponse>(external);
		var skills = await Uow.Skill.EntitySet.ToListAsync();
		var skillIds = externalResponse.SkillIds.Split(';');

		externalResponse.SkillNames = string.Join(
		"; ",
			skillIds.Select((skillId) => skills
				.First(skill => skill.Id == int.Parse(skillId)).Name)
		);

		return Ok(externalResponse);
	}

	/// <summary>
	/// 	Add an external
	/// </summary>
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		await Repo.DeleteByIdAsync(id);
		return Ok();
	}

	/// <summary>
	/// 	Update an external's skills
	/// </summary>
	[HttpPut("{id:int}/{skillIds}")]
	public async Task<IActionResult> Update(int id, string skillIds)
	{
		var existingExternal = await Repo.GetByIdAsync(id);
		existingExternal.ThrowIfNullHttpStatus("Cannot find external");

		existingExternal.SkillIds = skillIds;
		await Repo.UpdateAsync(existingExternal);

		var skillIdList = skillIds.Split(';');
		var addedClasses = skillIdList.Select(
			_ =>
				new Class
				{
					Name = existingExternal.Name,
					Placement = existingExternal.Name,
					StartDate = DateTime.UtcNow,
					Available = true,
					EnableAutomation = false,
					SkillId = int.Parse(_)
				}
		);

		await Uow.Class.InsertRangeAsync(addedClasses);

		return Ok();
	}

	/// <summary>
	/// 	re-generate credential of external
	/// </summary>
	[HttpPut("{id:int}/regenerate")]
	public async Task<IActionResult> RegenerateCredential(int id)
	{
		static string getUniqueString() => Guid.NewGuid().ToString();

		var existingExternal = await Repo.GetByIdAsync(id);
		existingExternal.ThrowIfNullHttpStatus("Cannot find external");

		existingExternal.PassCode = getUniqueString() + getUniqueString();
		await Repo.UpdateAsync(existingExternal);

		return Ok(existingExternal);
	}
}
