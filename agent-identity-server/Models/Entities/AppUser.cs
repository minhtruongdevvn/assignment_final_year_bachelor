namespace AgentIdentityServer.Models.Entities;

public class AppUser : IdentityUser<int>
{
	public string? InternalCode { get; set; }
	public string? FamilyName { get; set; }
	public string? GivenName { get; set; }
	public string? BelongTo { get; set; }
}
