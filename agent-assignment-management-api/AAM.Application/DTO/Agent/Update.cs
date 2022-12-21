namespace AAM.Application.DTO.Agent;

internal class Update
{
    public string FamilyName { get; set; } = string.Empty;
    public string GivenName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string? Picture { get; set; }
    public bool Sex { get; set; }
    public int SelfDiscipline { get; set; }
    public string Code { get; set; } = string.Empty;
    public int Age { get; set; }
    public double Height { get; set; }
    public double IQ { get; set; }
    public double EQ { get; set; }
    public double Stamina { get; set; }
    public double Strength { get; set; }
    public double ReactionTime { get; set; }
}

