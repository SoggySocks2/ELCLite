using ELCLite.SharedKernel.BaseClasses;
using System.Data;
using System.Globalization;
using System.Net.Mail;

namespace ELCLite.Identity.Data.Entities
{
    public class User : BaseEntity
    {
        private User() { }

        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string NormalizedEmail { get; private set; } = string.Empty;
        public string PreferredLanguageISOCode { get; private set; } = string.Empty;
        public bool IsStaff { get; private set; }

        public IEnumerable<UserRole> UserRoles => _userRoles.AsEnumerable();
        private readonly List<UserRole> _userRoles = [];

        public IEnumerable<Role> Roles => UserRoles.Select(x => x.Role);

        public string FullName => $"{FirstName} {LastName}";

        public User(string email, string firstName, string lastName, string preferredLanguageISOCode = "en-US", bool isStaff = true)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException($"{nameof(email)} is required");

            var emailAddress = new MailAddress(email);
            Email = emailAddress.Address;
            NormalizedEmail = Email.Normalize().ToUpperInvariant();

            // Allow empty firstname and lastname. Initially admins may want to create the user just by typing the email address.
            // Then, these values will be populated during the sign-up process by the user.
            Update(firstName, lastName, preferredLanguageISOCode, isStaff);
        }

        public void Update(string firstName, string lastName, string preferredLanguageISOCode = "en-US", bool isStaff = true)
        {
            FirstName = firstName;
            LastName = lastName;
            IsStaff = isStaff;

            var cultureInfo = new CultureInfo(preferredLanguageISOCode);
            PreferredLanguageISOCode = cultureInfo.Name;
        }

        public void AssignToRole(Guid roleId)
        {
            if (!UserRoles.Any(x => x.RoleId == roleId))
            {
                var userRole = new UserRole(Id, roleId);
                _userRoles.Add(userRole);
            }
        }

        public void AssignToRoles(IEnumerable<Guid> roleIds)
        {
            if (roleIds is null) return;

            foreach (var roleId in roleIds)
            {
                AssignToRole(roleId);
            }
        }

        public void RemoveFromRole(Guid roleId)
        {
            var userRole = _userRoles.Find(v => v.RoleId == roleId);

            if (userRole is null) return;

            _userRoles.Remove(userRole);
        }

        public void UpdateRoles(IEnumerable<Guid> roleIds)
        {
            if (roleIds is null) return;

            var userRolesToRemove = UserRoles.Where(x => !roleIds.Contains(x.RoleId)).ToList();
            foreach (var userRoleToRemove in userRolesToRemove)
            {
                _userRoles.Remove(userRoleToRemove);
            }

            foreach (var roleId in roleIds)
            {
                AssignToRole(roleId);
            }
        }
    }
}
