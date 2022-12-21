namespace AtmAPI.Models.DTOs.Skill;

public class SkillResponse : ResponseBase
{
	public string Name { get; set; } = default!;
	public string? Description { get; set; }
	public CategoryResponse? Category { get; set; }
}
