namespace AtmAPI.Models.DTOs.ServiceProviders;

public class UserUpsertRequest
{
	[Required]
	public string Email { get; set; } = default!;

	[Required]
	public string Password { get; set; } = default!;
}
