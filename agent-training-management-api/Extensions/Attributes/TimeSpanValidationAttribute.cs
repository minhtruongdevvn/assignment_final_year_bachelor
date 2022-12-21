namespace AtmAPI.Extensions.Attributes;

public class TimeSpanValidationAttribute : ValidationAttribute
{
	private readonly RangeValidationTypes _type;
	private readonly TimeSpan _value;

	public TimeSpanValidationAttribute(RangeValidationTypes type, int hours, int mins, int secs)
	{
		_type = type;
		_value = new(hours, mins, secs);
	}

	protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
	{
		var timeSpanValue = (TimeSpan)value!;
		var result = !IsValid(timeSpanValue)
			? new($"{validationContext.DisplayName} must be {_type.GetDescription()} {_value}")
			: ValidationResult.Success;

		return result!;
	}

	private bool IsValid(TimeSpan input) =>
		_type == RangeValidationTypes.Min ? _value <= input : _value >= input;
}
