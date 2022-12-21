namespace AtmAPI.Models.DTOs.ExternalInstitution;

public class ExternalAgentUpdateVerifiedRequest : ExternalRequest
{
	public AgentUpdateVerifiedRequest Agent { get; set; } = default!;
}
