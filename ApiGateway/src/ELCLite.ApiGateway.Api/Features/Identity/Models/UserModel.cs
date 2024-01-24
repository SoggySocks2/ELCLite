namespace ELCLite.ApiGateway.Api.Features.Identity.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsStaff { get; set; }
        public string LanguageCode { get; set; }

        public List<RoleNameModel> Roles { get; set; }
    }
}
