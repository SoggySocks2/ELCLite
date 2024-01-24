namespace ELCLite.Identity.Api.Features.Users
{
    public class CreateUserDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsStaff { get; set; }
        public string LanguageCode { get; set; } = string.Empty;

        public List<Guid> RoleIds { get; set; } = [];
    }
}
