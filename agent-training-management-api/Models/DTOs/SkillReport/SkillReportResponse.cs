namespace AtmAPI.Models.DTOs.SkillReport;

public class SkillReportResponse : ResponseBase
{
	public int StudentId { get; set; }
	public int Score { get; set; }
	public string? Status { get; set; }
	public bool Editable { get; set; } = true;
	public string? Metadata { get; set; }
	public StudentResponse? Student { get; set; }
	public ClassResponse? Class { get; set; }
	public SkillResponse? Skill { get; set; }
}
