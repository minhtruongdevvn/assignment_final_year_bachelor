namespace AgentIdentityServer.Controllers;

[ApiController]
[Authorize(LocalApi.PolicyName)]
[Route("api/[controller]s")]
public class UserController : ControllerBase
{
	private readonly RoleManager<AppRole> _roleManager;
	private readonly UserManager<AppUser> _userManager;
	private readonly ISieveProcessor _sieveProcessor;

	public UserController(
		UserManager<AppUser> userManager,
		RoleManager<AppRole> roleManager,
		ISieveProcessor sieveProcessor
	)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_sieveProcessor = sieveProcessor;
	}

	[HttpPost("student")]
	public async Task<IActionResult> CreateStudentAsync([FromBody] UserRequest request) =>
		await HandleCreateAsync(request, new[] { "agent" });

	[HttpPost("lecturer")]
	public async Task<IActionResult> CreateLecturerAsync([FromBody] UserRequest request) =>
		await HandleCreateAsync(request, new[] { "lecturer" });

	[HttpPost("operator")]
	public async Task<IActionResult> CreateOperatorAsync([FromBody] UserRequest request) =>
		await HandleCreateAsync(request, new[] { "operator" });

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteAsync(string id)
	{
		var user = await _userManager.FindByIdAsync(id);
		if (user == null)
			return Ok();

		var delete = await _userManager.DeleteAsync(user);
		return !delete.Succeeded ? BadRequest(delete.Errors) : Ok();
	}

	[HttpGet]
	public async Task<IActionResult> FindAsync([FromQuery] SieveModel request)
	{
		var sortedQuery = _sieveProcessor.Apply(request, _userManager.Users);
		sortedQuery.ThrowIfNull().IfEmpty();

		var users = new List<UserResponse>();
		foreach (var u in sortedQuery)
			users.Add(new(u, await _userManager.GetRolesAsync(u)));
		return Ok(users);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> FindAsync(string id)
	{
		var user = await _userManager.FindByIdAsync(id);
		return Ok(new UserResponse(user, await _userManager.GetRolesAsync(user)));
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateAsync([FromBody] UserRequest request, string id)
	{
		var userToUpdate = await _userManager.FindByIdAsync(id);
		if (userToUpdate == null)
			return BadRequest(new string[] { $"User with Id: {id} not found" });

		userToUpdate.UserName = request.Email;
		userToUpdate.Email = request.Email;
		userToUpdate.InternalCode = request.InternalCode ?? userToUpdate.InternalCode;
		userToUpdate.GivenName = request.GivenName ?? userToUpdate.GivenName;
		userToUpdate.FamilyName = request.FamilyName ?? userToUpdate.FamilyName;
		userToUpdate.BelongTo = request.BelongTo ?? userToUpdate.BelongTo;

		var update = await _userManager.UpdateAsync(userToUpdate);
		if (!update.Succeeded)
			return BadRequest(update.Errors);

		var userRoles = await _userManager.GetRolesAsync(userToUpdate);
		return Ok(new UserResponse(userToUpdate, userRoles));
	}

	[HttpPatch("{id}/roles")]
	public async Task<IActionResult> AssignUserToRoleAsync(
		[FromBody] AssignRolesRequest request,
		string id
	)
	{
		var user = await _userManager.FindByIdAsync(id);
		if (user == null)
			return BadRequest();

		var add = await _userManager.AddToRolesAsync(user, request.Roles);
		return !add.Succeeded ? BadRequest() : Ok();
	}

	[HttpDelete("{userId}/roles/{roleName}")]
	public async Task<IActionResult> RemoveUserRoleAsync(string userId, string roleName)
	{
		var user = await _userManager.FindByIdAsync(userId);
		if (user == null)
			return BadRequest();

		var delete = await _userManager.RemoveFromRoleAsync(user, roleName);
		return !delete.Succeeded ? BadRequest() : Ok();
	}

	[HttpGet("{id}/roles")]
	public async Task<IActionResult> FindUserRolesAsync(string id)
	{
		var user = await _userManager.FindByIdAsync(id);
		if (user == null)
			return BadRequest();

		var userRoles = await _userManager.GetRolesAsync(user);
		var roles = await _roleManager.Roles.Where(_ => userRoles.Contains(_.Name)).ToArrayAsync();

		return !roles.Any()
			? BadRequest()
			: Ok(
				roles.Select(
					r =>
						new RoleResponse
						{
							Id = r.Id,
							Name = r.Name,
							Description = r.Description
						}
				)
			);
	}

	[HttpPatch("{id}/password")]
	public async Task<IActionResult> ChangeUserPasswordAsync(
		string id,
		ChangePasswordRequest request
	)
	{
		var user = await _userManager.FindByIdAsync(id);
		if (user == null)
			return BadRequest(
				new
				{
					code = "UserNotFound",
					description = "could not found user with the given ID"
				}
			);

		var result = await _userManager.ChangePasswordAsync(
			user,
			request.OldPassword,
			request.NewPassword
		);
		return !result.Succeeded ? BadRequest(result.Errors) : Ok();
	}

	private async Task<IActionResult> HandleCreateAsync(UserRequest request, string[] roles)
	{
		request.Email.ThrowIfNull().IfNotValidEmail();

		var create = await _userManager.CreateAsync(
			new()
			{
				Email = request.Email,
				UserName = request.Email,
				InternalCode = request.InternalCode,
				FamilyName = request.FamilyName,
				GivenName = request.GivenName,
				BelongTo = request.BelongTo
			},
			request.Password
		);

		if (!create.Succeeded)
			return BadRequest(create.Errors);

		var user = await _userManager.FindByEmailAsync(request.Email);

		var addedUser = await _userManager.AddToRolesAsync(user, roles);
		return !addedUser.Succeeded ? BadRequest(addedUser.Errors) : Ok(new UserResponse(user));
	}
}
