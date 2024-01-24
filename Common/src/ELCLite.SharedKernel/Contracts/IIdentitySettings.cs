namespace ELCLite.SharedKernel.Contracts
{
    public interface IIdentitySettings
    {
        bool DisableAzureKeyVault { get; set; }
        string AzureKeyVaultUrl { get; set; }
        string AzureKeyVaultSecretKey { get; set; }
        string EncryptionKey { get; set; }
        string DbConnectionString { get; set; }
        int? DbTimeout { get; set; }
    }
}