namespace AtmAPI.Extensions.Attributes;

public class NumericValidationAttribute : ValidationAttribute
{
	private readonly RangeValidationTypes _types;
	private readonly int _value;

	public NumericValidationAttribute(RangeValidationTypes types, int value)
	{
		_value = value;
		_types = types;
	}

	protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
	{
		var numericValue = (int)value!;
		var result = !IsValid(numericValue)
			? new($"{validationContext.DisplayName} must be {_types.GetDescription()} {_value}")
			: ValidationResult.Success;

		return result!;
	}

	private bool IsValid(int input) =>
		_types == RangeValidationTypes.Min ? _value <= input : _value >= input;
}
