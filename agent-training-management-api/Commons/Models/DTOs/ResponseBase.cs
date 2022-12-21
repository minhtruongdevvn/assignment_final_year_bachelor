namespace AtmAPI.Commons.Models.DTOs;

public abstract class ResponseBase
{
	public int Id { get; set; }

	[MaxLength(ModelConstants.Commons.MaxLength)]
	public string? CreatedBy { get; set; }

	[MaxLength(ModelConstants.Commons.MaxLength)]
	public string? UpdatedBy { get; set; }

	public DateTime? CreatedAt { get; set; }

	public DateTime? UpdatedAt { get; set; }
}
