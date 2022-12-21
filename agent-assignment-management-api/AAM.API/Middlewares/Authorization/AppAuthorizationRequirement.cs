using Microsoft.AspNetCore.Authorization;

namespace AAM.API.Authorization;

public class AppAuthorizationRequirement : IAuthorizationRequirement
{
    public string RequiredScope { get; }
    public string RequiredRole { get; }
    public string RequiredBelongTo { get; }
    public AppAuthorizationRequirement(string requireScope, string requireRole, string requiredBelongTo)
    {
        RequiredScope = requireScope;
        RequiredRole = requireRole;
        RequiredBelongTo = requiredBelongTo;
    }
}

public class AppAuthorization : AuthorizeAttribute
{
    public AppAuthorization()
    {
        Policy = AuthConstant.AppPolicy;
    }
}