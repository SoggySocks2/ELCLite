using Microsoft.AspNetCore.Authorization;

namespace ELCLite.ApiGateway.Api.Configuration.Authorization
{
    public static class PermissionAuthorizationRequirementExtensions
    {
        public static AuthorizationPolicyBuilder RequirePermission(
            this AuthorizationPolicyBuilder authorizationPolicyBuilder,
            int requiredPermission)
        {
            authorizationPolicyBuilder.AddRequirements(new PermissionAuthorizationRequirement(requiredPermission));
            return authorizationPolicyBuilder;
        }
    }
}
