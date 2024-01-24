using Microsoft.AspNetCore.Identity;

namespace ELCLite.Identity.Infrastructure
{
    public class ElcUser : IdentityUser
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string AccountType { get; private set; } = string.Empty;
        public string AccountTypeIdentifier { get; private set; } = string.Empty;
    }
}
