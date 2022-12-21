using Microsoft.AspNetCore.Authorization;

namespace AAM.API.Authorization;

public class AppAuthorizationHandler : AuthorizationHandler<AppAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AppAuthorizationRequirement requirement)
    {
        string belongToClaimField = "belong_to";

        if (!(context.User.HasClaim(c => c.Type == "scope") && context.User.HasClaim(c => c.Type == belongToClaimField)))
            return Task.CompletedTask;

        var scopes = context.User.FindAll(c => c.Type == "scope");

        if (
            context.User.IsInRole(requirement.RequiredRole) && 
            scopes.Any(s => s.Value == requirement.RequiredScope) &&
            context.User.Claims.First(x => x.Type == belongToClaimField).Value == requirement.RequiredBelongTo
        )
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}

