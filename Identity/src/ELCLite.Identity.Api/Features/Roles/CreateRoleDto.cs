namespace ELCLite.Identity.Api.Features.Roles
{
    public class CreateRoleDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<int> SelectedPermissionIds { get; set; } = [];
    }
}
