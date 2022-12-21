namespace AtmAPI.Models.DTOs.Student;

public class StudentInsertRequest : IdentityUpsertBase
{
	[Required]
	public string Password { get; set; } = default!;
	public bool Sex { get; set; } // true: female, false: male
	public int SelfDiscipline { get; set; } = 5;
	public int Age { get; set; }
	public double Height { get; set; }
	public double IQ { get; set; }
	public double EQ { get; set; }
	public double Stamina { get; set; }
	public double Strength { get; set; }
	public double ReactionTime { get; set; }
	public string IdentifyNumber { get; set; } = default!;
}
