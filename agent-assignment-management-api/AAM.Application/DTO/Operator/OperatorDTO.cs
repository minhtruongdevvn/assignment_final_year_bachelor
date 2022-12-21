using System.ComponentModel.DataAnnotations;

namespace AAM.Application;

public class OperatorDTO
{
    [Required]
    public string Email { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;

    [Required]
    public string FamilyName { get; set; } = default!;

    [Required]
    public string GivenName { get; set; } = default!;

    [Required]
    public string InternalCode { get; set; } = default!;
    public string? BelongTo { get; set; }
}
