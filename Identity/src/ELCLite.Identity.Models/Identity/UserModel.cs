namespace ELCLite.Identity.Models.Identity
{
    public record UserModel
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TelNo { get; set; } = string.Empty;
    }
}
