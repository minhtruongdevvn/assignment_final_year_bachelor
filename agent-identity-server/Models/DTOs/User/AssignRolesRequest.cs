namespace AgentIdentityServer.Models.DTOs.User;

public class AssignRolesRequest
{
	[Required]
	public string[] Roles { get; set; } = default!;
}
