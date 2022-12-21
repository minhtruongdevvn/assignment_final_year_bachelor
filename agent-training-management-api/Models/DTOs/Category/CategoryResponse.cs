namespace AtmAPI.Models.DTOs.Category;

public class CategoryResponse : ResponseBase
{
	public string Name { get; set; } = default!;
	public string? Description { get; set; }
}
