namespace ELCLite.Identity.Models.Identity
{
    public record ResetPasswordModel
    {
        public string Email { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
