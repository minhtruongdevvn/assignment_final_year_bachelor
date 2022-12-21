namespace AtmAPI.Commons.Models.DTOs;

public abstract class IdentityDtoBase : ResponseBase
{
	public virtual string? UserName { get; set; }

	[Required]
	public string Email { get; set; } = default!;

	[MaxLength(ModelConstants.Commons.MaxLength)]
	public string Picture { get; set; } = default!;

	[Required]
	public string FamilyName { get; set; } = default!;

	[Required]
	public string GivenName { get; set; } = default!;

	[Required]
	public DateTime BirthDate { get; set; } = DateTime.Today;

	public string Code { get; set; } = default!;
	public string? Password { get; set; }
}
