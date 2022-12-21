namespace AtmAPI.Models.DTOs.Student;

public class StudentUpdateRequest
{
	public bool Sex { get; set; }
	public int Age { get; set; }
	public string IdentifyNumber { get; set; } = default!;

	[Required]
	[EmailAddress]
	public string Email { get; set; } = default!;

	[Required]
	public string FamilyName { get; set; } = default!;

	[Required]
	public string GivenName { get; set; } = default!;

	public DateTime BirthDate { get; set; } = DateTime.Today;
}
