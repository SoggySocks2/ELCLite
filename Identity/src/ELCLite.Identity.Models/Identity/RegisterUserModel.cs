namespace ELCLite.Identity.Models.Identity
{
    public record RegisterUserModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string TelNo { get; set; } = string.Empty;
    }
}
