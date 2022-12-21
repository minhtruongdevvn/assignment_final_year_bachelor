namespace AtmAPI.Models.DTOs.Operator;

public class OperatorInsertRequest : IdentityUpsertBase
{
	[Required]
	public string Password { get; set; } = default!;
}
