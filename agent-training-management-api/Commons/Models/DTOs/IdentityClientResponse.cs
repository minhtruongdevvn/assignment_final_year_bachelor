namespace AtmAPI.Commons.Models.DTOs;

public class IdentityClientResponse
{
	public HttpStatusCode StatusCode { get; set; }
	public dynamic? Content { get; set; }
}
