namespace AtmAPI.Models.Entities;

public class SkillReport : EntityBase
{
	[Range(0, 100)]
	public int Score { get; set; }
	public SkillReportTypes Status { get; set; } = SkillReportTypes.InProgress;
	public bool Editable { get; set; } = true;
	public string? Metadata { get; set; }
	public int ClassId { get; set; }
	public int StudentId { get; set; }

	public virtual Class Class { get; set; } = default!;
	public virtual Student Student { get; set; } = default!;
}
