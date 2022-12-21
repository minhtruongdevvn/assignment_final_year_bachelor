namespace AtmAPI.Commons.Models.Entities;

public abstract class IdentityBase : EntityBase
{
	public string? UserName { get; set; }
	public string Email { get; set; } = default!;
	public string FamilyName { get; set; } = default!;
	public string GivenName { get; set; } = default!;
	public DateTime BirthDate { get; set; } = DateTime.Today;

	[MaxLength(ModelConstants.Commons.MaxLength)]
	public string Picture { get; set; } = default!;

	[MaxLength(ModelConstants.Commons.CategoryNameLength)]
	public string Code { get; set; } = default!;

	public string IdentityReference { get; set; } = default!;
}
