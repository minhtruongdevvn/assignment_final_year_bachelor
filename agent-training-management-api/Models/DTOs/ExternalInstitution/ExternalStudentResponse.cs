namespace AtmAPI.Models.DTOs.ExternalInstitution;

public class ExternalStudentResponse
{
	public string ExternalCode { get; set; } = string.Empty;
	public StudentResponse Student = default!;
}
