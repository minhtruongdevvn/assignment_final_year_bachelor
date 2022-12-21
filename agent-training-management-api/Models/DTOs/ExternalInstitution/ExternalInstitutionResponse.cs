namespace AtmAPI.Models.DTOs.ExternalInstitution;

public class ExternalInstitutionResponse : ResponseBase
{
	public string Name { get; set; } = default!;
	public string Code { get; set; } = default!;

	// seperator: ";"
	public string SkillIds { get; set; } = string.Empty;

	public string SkillNames { get; set; } = string.Empty;
}
