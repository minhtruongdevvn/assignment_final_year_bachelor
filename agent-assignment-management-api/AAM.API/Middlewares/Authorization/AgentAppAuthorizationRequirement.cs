using Microsoft.AspNetCore.Authorization;

namespace AAM.API.Authorization;

public class AgentAppAuthorizationRequirement : IAuthorizationRequirement
{
    public string RequiredScope { get; }
    public string RequiredRole { get; }
    public AgentAppAuthorizationRequirement(string requireScope, string requireRole)
    {
        RequiredScope = requireScope;
        RequiredRole = requireRole;
    }
}

public class AgentAppAuthorization : AuthorizeAttribute
{
    public AgentAppAuthorization()
    {
        Policy = AuthConstant.AgentAppPolicy;
    }
}