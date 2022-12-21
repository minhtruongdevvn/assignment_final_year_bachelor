using AtmAPI.Models.DTOs.Lecturer;

namespace AtmAPI.Models.DTOs.ClassLecturer;

public class ClassLecturerResponse
{
	public ClassResponse? Class { get; set; }
	public LecturerResponse? Lecturer { get; set; }
}
