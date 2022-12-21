namespace AtmAPI.Models.DTOs.SkillReport;

public class SkillReportUpdateVerifiedRequest
{
	[Required]
	[Range(0, 100)]
	public int Score { get; set; } = 0;

	[Required]
	public int ClassId { get; set; }
}
