using AtmAPI.Models.DTOs.Category;

namespace AtmAPI.Controllers.Managements;

[Route("categories")]
public class CategoryController : ManagementControllerBase<Category>
{
	public CategoryController(
		ISieveProcessor sieveProc,
		AtmContext context,
		IMapper mapper,
		IUnitOfWork uow
	) : base(sieveProc, context, mapper, uow) { }

	/// <summary>
	/// 	Get categories
	/// </summary>
	[HttpGet]
	public async Task<IActionResult> Get([FromQuery] SieveModel request) =>
		await HandleGetAsync<Category, CategoryResponse>(request);

	/// <summary>
	/// 	Get a category by id
	/// </summary>
	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetById(int id)
	{
		var category = await Repo.GetByIdAsync(id);
		category.ThrowIfNullHttpStatus();
		return Ok(Mapper.Map<CategoryResponse>(category));
	}

	/// <summary>
	/// 	Update a category
	/// </summary>
	[HttpPut("{id:int}")]
	public async Task<IActionResult> Put([FromBody] CategoryUpsertRequest request, int id)
	{
		var categoryToUpdate = await Repo.GetByIdAsync(id);
		categoryToUpdate.ThrowIfNullHttpStatus();

		Mapper.Map(request, categoryToUpdate);
		await Repo.UpdateAsync(categoryToUpdate);

		return Ok();
	}

	/// <summary>
	/// 	Add a category
	/// </summary>
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] CategoryUpsertRequest request)
	{
		var createdCategory = await Repo.InsertAsync(Mapper.Map<Category>(request));
		return Ok(Mapper.Map<CategoryResponse>(createdCategory));
	}

	/// <summary>
	/// 	Delete a category
	/// </summary>
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		await Repo.DeleteByIdAsync(id);
		return Ok();
	}
}
