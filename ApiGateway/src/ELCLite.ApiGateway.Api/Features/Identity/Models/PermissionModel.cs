namespace ELCLite.ApiGateway.Api.Features.Identity.Models
{
    public class PermissionModel
    {
        public int Id { get; set; }
        public bool Value { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
    }
}
