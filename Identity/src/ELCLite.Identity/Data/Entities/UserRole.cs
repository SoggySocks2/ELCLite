namespace ELCLite.Identity.Data.Entities
{
    public class UserRole
    {
        private UserRole() { }

        public Guid UserId { get; private set; }
        public Guid RoleId { get; private set; }
        public bool IsDeleted { get; private set; }


        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public Guid ModifiedBy { get; set; }

        public User? User { get; private set; }
        public Role? Role { get; private set; }

        public UserRole(Guid userId, Guid roleId)
        {
            // Do not check for Guid.Empty, EF will recognize it as a new record.

            UserId = userId;
            RoleId = roleId;
        }
    }
}
