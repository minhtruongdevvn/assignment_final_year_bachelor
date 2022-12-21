namespace AtmAPI.Models.DTOs.Skill;

public class SkillUpsertRequest
{
	[Required]
	[RegularExpression(@"^[_a-z]+$", ErrorMessage = "Only lowercase and '_' are allowed.")]
	[MaxLength(ModelConstants.Commons.MaxLength)]
	public string? Name { get; set; }

	[MaxLength(ModelConstants.Commons.DescriptionLength)]
	public string? Description { get; set; }

	[Required]
	public int CategoryId { get; set; }
}
