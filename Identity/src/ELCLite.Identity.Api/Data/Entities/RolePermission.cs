using ELCLite.SharedKernel.SharedObjects;

namespace ELCLite.Identity.Api.Data.Entities
{
    public class RolePermission
    {
        private RolePermission() { }

        public int Id { get; private set; }
        public Permission? Permission { get; set; }

        public Guid RoleId { get; private set; }
        public Role? Role { get; private set; }

        public RolePermission(PermissionKey permissionKey, bool permissionValue)
        {
            Permission = new Permission(permissionKey, permissionValue);
        }

        public RolePermission(Permission permission)
        {
            if (permission is null) throw new ArgumentException($"{nameof(permission)} is required");

            Permission = permission;
        }
    }
}
