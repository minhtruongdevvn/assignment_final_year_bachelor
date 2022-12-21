namespace AtmAPI.Models.Entities;

public class Skill : EntityBase
{
	[MaxLength(ModelConstants.Commons.MaxLength)]
	public string Name { get; set; } = default!;

	[MaxLength(ModelConstants.Commons.DescriptionLength)]
	public string? Description { get; set; }
	public int CategoryId { get; set; } = default!;

	public virtual Category Category { get; set; } = default!;
	public virtual ICollection<Class>? Classes { get; set; }
}
