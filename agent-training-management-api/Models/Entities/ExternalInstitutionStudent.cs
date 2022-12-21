namespace AtmAPI.Models.Entities;

public class ExternalInstitutionStudent : EntityMetadataBase
{
	public int StudentId { get; set; }
	public int ExternalInstitutionId { get; set; }

	public virtual Student Student { get; set; } = default!;
	public virtual ExternalInstitution ExternalInstitution { get; set; } = default!;
}
