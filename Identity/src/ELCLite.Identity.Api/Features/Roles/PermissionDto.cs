namespace ELCLite.Identity.Api.Features.Roles
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public bool Value { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
    }
}
