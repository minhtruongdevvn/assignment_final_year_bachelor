namespace AgentIdentityServer.Models.DTOs.User;

public class UserResponse
{
	public UserResponse() { }

	public UserResponse(AppUser user, IList<string> userRoles)
	{
		Id = user.Id;
		UserName = user.UserName;
		Email = user.Email;
		EmailConfirmed = user.EmailConfirmed;
		PhoneNumber = user.PhoneNumber;
		PhoneNumberConfirmed = user.PhoneNumberConfirmed;
		Roles = userRoles.ToArray();
	}

	public UserResponse(AppUser user)
	{
		Id = user.Id;
		UserName = user.UserName;
		Email = user.Email;
		EmailConfirmed = user.EmailConfirmed;
		PhoneNumber = user.PhoneNumber;
		PhoneNumberConfirmed = user.PhoneNumberConfirmed;
		Roles = null;
	}

	public int? Id { get; set; }
	public string? UserName { get; set; }
	public string[]? Roles { get; set; }
	public string? Email { get; set; }
	public bool EmailConfirmed { get; set; }
	public string? PhoneNumber { get; set; }
	public bool PhoneNumberConfirmed { get; set; }
}
