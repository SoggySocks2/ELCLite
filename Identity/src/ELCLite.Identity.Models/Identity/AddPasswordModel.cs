namespace ELCLite.Identity.Models.Identity
{
    public record AddPasswordModel
    {
        public string Email { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
