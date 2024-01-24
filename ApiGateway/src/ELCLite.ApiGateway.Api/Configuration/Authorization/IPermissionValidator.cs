using ELCLite.ApiGateway.Api.Features.Identity.Models;

namespace ELCLite.ApiGateway.Api.Configuration.Authorization
{
    public interface IPermissionValidator
    {
        void UpdateRoles(List<RoleModel> roles);
        bool ValidateForRoles(int requiredPermissionKey, string[] roles);
    }
}
