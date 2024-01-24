using ELCLite.Identity.Api.Data.Entities;

namespace ELCLite.Identity.Api.Data.Seeds
{
    public static class RoleSeed
    {
        public static string GlobalAdminRoleNameNormalized = "GlobalAdmin".Normalize().ToUpperInvariant();

        public static List<Role> GetRoles()
        {
            var permissions = PermissionSeed.GeneratePermissionsForAdmin();
            var globalAdminRole = new Role("GlobalAdmin", "Global admin role");
            globalAdminRole.ClearAndAddPermissions(permissions);

            return new List<Role>
            {
                globalAdminRole,
                new("Admin", "Admin role"),
                new("RetailerAdmin", "Retailer admin role"),
                new("Retailer", "Retailer role"),
            };
        }
    }
}
