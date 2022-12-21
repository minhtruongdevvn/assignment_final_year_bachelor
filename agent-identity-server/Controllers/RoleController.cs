namespace AgentIdentityServer.Controllers;

[ApiController]
[Authorize(LocalApi.PolicyName)]
[Route("api/[controller]s")]
public class RoleController : ControllerBase
{
	private readonly RoleManager<AppRole> _roleManager;

	public RoleController(RoleManager<AppRole> roleManager) => _roleManager = roleManager;

	[HttpPost]
	public async Task<IActionResult> CreateAsync(RoleRequest request)
	{
		var create = await _roleManager.CreateAsync(
			new() { Name = request.Name, Description = request.Description, }
		);
		return !create.Succeeded ? BadRequest() : Ok();
	}

	[HttpGet]
	public IActionResult Find() =>
		Ok(
			_roleManager.Roles.Select(
				_ =>
					new RoleResponse
					{
						Id = _.Id,
						Name = _.Name,
						Description = _.Description
					}
			)
		);

	[HttpGet("{id}")]
	public async Task<IActionResult> FindAsync(string id)
	{
		var role = await _roleManager.FindByIdAsync(id);
		return Ok(
			new RoleResponse
			{
				Id = role.Id,
				Name = role.Name,
				Description = role.Description
			}
		);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateAsync(string id, RoleRequest request)
	{
		var role = await _roleManager.FindByIdAsync(id);
		if (role == null)
			return BadRequest();

		role.Name = request.Name;
		role.Description = request.Description;

		var update = await _roleManager.UpdateAsync(role);

		return !update.Succeeded ? BadRequest() : Ok();
	}
}
