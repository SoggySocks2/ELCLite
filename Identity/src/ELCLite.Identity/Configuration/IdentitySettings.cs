namespace ELCLite.Identity.Configuration
{
    public class IdentitySettings
    {
        public const string CONFIG_NAME = "IdentitySettings";

        public static IdentitySettings Instance { get; } = new IdentitySettings();
        private IdentitySettings() { }

        public string EncryptionKey { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
    }
}
