namespace AtmAPI.Models.DTOs.ExternalInstitution;

public class ExternalSkillReportUpdateVerifiedRequest : ExternalRequest
{
	public SkillReportUpdateVerifiedRequest SkillReport { get; set; } = default!;
}
