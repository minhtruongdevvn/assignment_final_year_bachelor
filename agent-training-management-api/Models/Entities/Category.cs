namespace AtmAPI.Models.Entities;

public class Category : EntityBase
{
	[MaxLength(ModelConstants.Commons.CategoryNameLength)]
	public string Name { get; set; } = default!;

	[MaxLength(ModelConstants.Commons.DescriptionLength)]
	public string? Description { get; set; }
}
