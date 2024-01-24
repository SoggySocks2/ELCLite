namespace ELCLite.Identity.Api.Features.Roles
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<PermissionDto> Permissions { get; set; } = [];
    }
}
