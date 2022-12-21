namespace AtmAPI.Commons.Models.Entities;

public abstract class EntityMetadataBase
{
	[MaxLength(ModelConstants.Commons.MaxLength)]
	public string CreatedBy { get; set; } = default!;

	[MaxLength(ModelConstants.Commons.MaxLength)]
	public string UpdatedBy { get; set; } = default!;

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
