using AtmAPI.Models.DTOs.Lecturer;

namespace AtmAPI.Models.DTOs.Class;

public class ClassResponse : ResponseBase
{
	public string Name { get; set; } = default!;
	public string? Description { get; set; }
	public string Placement { get; set; } = default!;
	public DateTime StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public bool? Available { get; set; }
	public bool EnableAutomation { get; set; }
	public int? MaxLearner { get; set; }
	public SkillResponse? Skill { get; set; }
	public IEnumerable<LecturerResponse>? Lecturers { get; set; }
}
