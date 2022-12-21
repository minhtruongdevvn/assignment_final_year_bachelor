namespace AtmAPI.Models.DTOs.ServiceProviders;

public class AgentUpdateVerifiedRequest
{
	public bool Sex { get; set; } // true: female, false: male
	public int SelfDiscipline { get; set; }
	public int Age { get; set; }
	public double Height { get; set; }
	public double IQ { get; set; }
	public double EQ { get; set; }
	public double Stamina { get; set; }
	public double Strength { get; set; }
	public double ReactionTime { get; set; }
}
