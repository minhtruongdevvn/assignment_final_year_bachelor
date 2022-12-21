namespace AtmAPI.Models.DTOs.ExternalInstitution;

public class ExternalSieveRequest : ExternalRequest
{
	public SieveModel Sieve { get; set; } = default!;
}
