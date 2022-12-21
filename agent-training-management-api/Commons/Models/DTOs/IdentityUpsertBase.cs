namespace AtmAPI.Commons.Models.DTOs;

public class IdentityUpsertBase
{
	[Required]
	[EmailAddress]
	public string Email { get; set; } = default!;

	[MaxLength(ModelConstants.Commons.MaxLength)]
	public string Picture { get; set; } = "none";

	[Required]
	public string FamilyName { get; set; } = default!;

	[Required]
	public string GivenName { get; set; } = default!;

	[Required]
	public DateTime BirthDate { get; set; } = DateTime.Today;
}
