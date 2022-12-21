namespace AAM.Application.DTO.Agent;

internal class Add
{
    public string Email { get; set; } = string.Empty;
    public string FamilyName { get; set; } = string.Empty;
    public string GivenName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string? Picture { get; set; }
    public bool Sex { get; set; }
    public string Code { get; set; } = string.Empty;
    public string IdentityReference { get; set; } = string.Empty;
    public int SelfDiscipline { get; set; }
    public int Age { get; set; }
    public double Height { get; set; }
    public double IQ { get; set; }
    public double EQ { get; set; }
    public double Stamina { get; set; }
    public double Strength { get; set; }
    public double ReactionTime { get; set; }
}

