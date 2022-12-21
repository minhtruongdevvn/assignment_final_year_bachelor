namespace AtmAPI.Models.DTOs.Category;

public class CategoryUpsertRequest
{
	[Required]
	[MaxLength(ModelConstants.Commons.CategoryNameLength)]
	[RegularExpression(@"^[_a-z]+$", ErrorMessage = "Only lowercase alphabet and '_' are allowed.")]
	public string Name { get; set; } = default!;

	[MaxLength(ModelConstants.Commons.DescriptionLength)]
	public string? Description { get; set; }
}
