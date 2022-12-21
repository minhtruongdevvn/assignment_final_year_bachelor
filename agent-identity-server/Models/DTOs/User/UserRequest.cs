namespace AgentIdentityServer.Models.DTOs.User;

public class UserRequest
{
	public string? Email { get; set; }
	public string? Password { get; set; }
	public string? FamilyName { get; set; }
	public string? GivenName { get; set; }
	public string? InternalCode { get; set; }
	public string? BelongTo { get; set; }
}
