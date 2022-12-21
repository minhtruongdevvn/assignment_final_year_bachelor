namespace AgentIdentityServer.Models.Entities;

public class AppRole : IdentityRole<int>
{
	public string? Description { get; set; }
}
