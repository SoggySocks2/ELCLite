using ELCLite.SharedKernel.Contracts;

namespace ELCLite.Identity.Api.Configuration
{
    public class IdentitySettings : IIdentitySettings
    {
        public const string CONFIG_NAME = "IdentitySettings";

        public static IdentitySettings Instance { get; } = new IdentitySettings();
        private IdentitySettings() { }

        public bool DisableAzureKeyVault { get; set; }
        public string AzureKeyVaultUrl { get; set; } = string.Empty;
        public string AzureKeyVaultSecretKey { get; set; } = string.Empty;
        public string EncryptionKey { get; set; } = string.Empty;
        public string DbConnectionString { get; set; } = string.Empty;
        public int? DbTimeout { get; set; } = 30;
    }
}
