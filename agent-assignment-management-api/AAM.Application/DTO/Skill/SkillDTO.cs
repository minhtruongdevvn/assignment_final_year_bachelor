namespace AAM.Application;

public class SkillDTO : BaseDTO
{
    public string? OldName { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

