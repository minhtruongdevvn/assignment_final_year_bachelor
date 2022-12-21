namespace AgentIdentityServer.Models.DTOs.Role;

public class RoleRequest
{
	[Required]
	public string Name { get; set; } = default!;

	public string Description { get; set; } = default!;
}
