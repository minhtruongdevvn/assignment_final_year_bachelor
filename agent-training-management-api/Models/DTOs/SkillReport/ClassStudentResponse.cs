namespace AtmAPI.Models.DTOs.SkillReport;

public class ClassStudentResponse
{
	public ClassResponse? Class { get; set; }
	public IEnumerable<StudentResponse>? Student { get; set; }
}
