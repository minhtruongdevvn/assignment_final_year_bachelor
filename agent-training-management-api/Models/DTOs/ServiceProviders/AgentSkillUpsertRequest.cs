namespace AtmAPI.Models.DTOs.ServiceProviders;

public class AgentSkillUpsertRequest
{
	public double Score { get; set; }
	public string SkillName { get; set; } = string.Empty;
	public string IdentityReference { get; set; } = string.Empty;
}
