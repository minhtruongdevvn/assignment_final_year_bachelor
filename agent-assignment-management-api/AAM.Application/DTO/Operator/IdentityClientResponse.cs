using System.Net;

namespace AAM.Application;

public class IdentityClientResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public dynamic? Content { get; set; }
}
