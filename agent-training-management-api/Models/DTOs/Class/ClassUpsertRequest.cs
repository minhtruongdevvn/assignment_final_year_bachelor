namespace AtmAPI.Models.DTOs.Class;

public class ClassUpsertRequest
{
	[Required]
	[MaxLength(ModelConstants.Commons.MaxLength)]
	public string Name { get; set; } = default!;

	[MaxLength(ModelConstants.Commons.DescriptionLength)]
	public string? Description { get; set; }

	[Required]
	[MaxLength(ModelConstants.Commons.MaxLength)]
	public string Placement { get; set; } = default!;

	[Required]
	public DateTime StartDate { get; set; } = DateTime.UtcNow.AddDays(7);
	public DateTime? EndDate { get; set; }
	public bool EnableAutomation { get; set; }
	public int? MaxLearner { get; set; }
	public bool? Available { get; set; }

	[Required]
	public int SkillId { get; set; }
}
