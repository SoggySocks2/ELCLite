namespace ELCLite.Identity.Features.Users
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsStaff { get; set; }
        public string LanguageCode { get; set; } = string.Empty;

        public List<Guid> RoleIds { get; set; } = [];
    }
}
