using ELCLite.Identity.Api.Data.Seeds;
using ELCLite.SharedKernel.BaseClasses;
using ELCLite.SharedKernel.SharedObjects;

namespace ELCLite.Identity.Api.Data.Entities
{
    public class Role : BaseEntity
    {
        private Role() { }

        public string Name { get; private set; } = string.Empty;
        public string NormalizedName { get; private set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public IEnumerable<UserRole> UserRoles => _userRoles.AsEnumerable();
        private readonly List<UserRole> _userRoles = [];

        public IEnumerable<RolePermission> RolePermissions => _rolePermissions.AsEnumerable();
        private readonly List<RolePermission> _rolePermissions = [];

        public IEnumerable<User> Users => UserRoles.Select(x => x.User);
        public IEnumerable<Permission> SelectedPermissions => RolePermissions.Select(x => x.Permission).Where(x => x.Value == true);
        public IEnumerable<Permission> Permissions => PermissionSeed.GeneratePermissions(SelectedPermissions.Select(x => x.Key.Value));

        public Role(string name, string description)
        {
            Update(name, description);
        }

        public void Update(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException($"{nameof(name)} is required");

            Name = name;
            NormalizedName = name.Normalize().ToUpperInvariant();
            Description = description;
        }

        public void AssignUser(Guid userId)
        {
            if (!UserRoles.Any(x => x.UserId == userId))
            {
                var userRole = new UserRole(userId, Id);
                _userRoles.Add(userRole);
            }
        }

        public void RemoveUser(Guid userId)
        {
            var userRole = _userRoles.Find(v => v.UserId == userId);

            if (userRole is null) return;

            _userRoles.Remove(userRole);
        }

        public void ClearAndAddPermissions(IEnumerable<int> permissionIds)
        {
            if (permissionIds is null) return;
            var permissions = permissionIds.Select(x => new Permission(PermissionKey.FromValue(x), true));

            ClearAndAddPermissions(permissions);
        }

        /// <summary>
        /// It clears the existing list of permissions, and adds the permissions given in the argument.
        /// It adds only permissions whith value true.
        /// </summary>
        /// <param name="permissions">Permissions to add to the role</param>
        public void ClearAndAddPermissions(IEnumerable<Permission> permissions)
        {
            if (permissions is null) return;

            _rolePermissions.Clear();

            if (permissions is not null)
            {
                _rolePermissions.AddRange(permissions.Where(x => x.Value == true).Select(x => new RolePermission(x)));
            }
        }
    }
}
