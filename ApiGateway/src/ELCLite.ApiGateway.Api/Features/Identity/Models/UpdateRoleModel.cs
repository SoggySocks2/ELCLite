namespace ELCLite.ApiGateway.Api.Features.Identity.Models
{
    public class UpdateRoleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<int> SelectedPermissionIds { get; set; }
    }
}
