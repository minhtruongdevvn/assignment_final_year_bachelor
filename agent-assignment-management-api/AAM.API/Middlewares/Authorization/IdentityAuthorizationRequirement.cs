using Microsoft.AspNetCore.Authorization;

namespace AAM.API.Authorization;

public class IdentityAuthorizationRequirement : IAuthorizationRequirement
{
    public string RequiredScope { get; }
    public IdentityAuthorizationRequirement(string requireScope)
    {
        RequiredScope = requireScope;
    }
}

public class IdentityAuthorization : AuthorizeAttribute
{
    public IdentityAuthorization()
    {
        Policy = AuthConstant.IdentityPolicy;
    }
}

