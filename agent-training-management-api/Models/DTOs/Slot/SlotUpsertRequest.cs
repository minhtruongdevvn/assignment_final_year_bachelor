namespace AtmAPI.Models.DTOs.Slot;

public class SlotUpsertRequest : IValidatableObject
{
	[Required]
	public DayOfWeek DayOfWeek { get; set; }

	[Required]
	public TimeSpan StartAt { get; set; }

	[Required]
	public TimeSpan EndAt { get; set; }

	IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
	{
		return StartAt < EndAt
			? new List<ValidationResult>
			{
				new ValidationResult("EndAt must be greater than StartAt")
			}
			: (IEnumerable<ValidationResult>)new List<ValidationResult>();
	}
}
