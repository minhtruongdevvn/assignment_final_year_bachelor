namespace AtmAPI.Models.DTOs.Lecturer;

public class LecturerInsertRequest : IdentityUpsertBase
{
	[Required]
	public string Password { get; set; } = default!;
}
