namespace AtmAPI.Models.Entities;

public class ExternalInstitution : EntityBase
{
	public string Name { get; set; } = default!;
	public string Code { get; set; } = default!;
	public string PassCode { get; set; } = default!;

	// seperator: ";"
	public string SkillIds { get; set; } = string.Empty;

	public virtual ICollection<ExternalInstitutionStudent>? ExternalInstitutionStudents { get; set; }
}
