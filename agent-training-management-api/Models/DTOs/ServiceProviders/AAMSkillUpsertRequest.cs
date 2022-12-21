namespace AtmAPI.Models.DTOs.ServiceProviders;

public class AAMSkillUpsertRequest
{
	public string? OldName { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
}
