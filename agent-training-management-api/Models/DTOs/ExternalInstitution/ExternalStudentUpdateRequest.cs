namespace AtmAPI.Models.DTOs.ExternalInstitution;

public class ExternalStudentUpdateRequest : ExternalRequest
{
	[Required]
	public string Password { get; set; } = default!;

	[Required]
	[EmailAddress]
	public string Email { get; set; } = default!;

	[MaxLength(ModelConstants.Commons.MaxLength)]
	public string Picture { get; set; } = default!;

	[Required]
	public string FamilyName { get; set; } = default!;

	[Required]
	public string GivenName { get; set; } = default!;

	[Required]
	public DateTime BirthDate { get; set; } = DateTime.Today;
	public StudentUpdateRequest Student { get; set; } = default!;
}
