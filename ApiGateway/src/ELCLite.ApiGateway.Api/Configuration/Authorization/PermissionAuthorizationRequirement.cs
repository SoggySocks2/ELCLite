using Microsoft.AspNetCore.Authorization;

namespace ELCLite.ApiGateway.Api.Configuration.Authorization
{
    public class PermissionAuthorizationRequirement : AuthorizationHandler<PermissionAuthorizationRequirement>, IAuthorizationRequirement
    {
        public int RequiredPermissionKey { get; }

        public PermissionAuthorizationRequirement(int requiredPermissionKey)
        {
            RequiredPermissionKey = requiredPermissionKey;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            if (context.User is not null)
            {
                var rolesClaim = context.User.Claims.FirstOrDefault(
                    //c => c.Type.Equals("extension_Roles", StringComparison.InvariantCultureIgnoreCase));
                    c => c.Type.Equals("role", StringComparison.InvariantCultureIgnoreCase));

                if (rolesClaim is not null)
                {
                    var roles = rolesClaim.Value.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    if (PermissionValidator.Instance.ValidateForRoles(RequiredPermissionKey, roles))
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
