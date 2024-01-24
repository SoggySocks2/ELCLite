using ELCLite.Identity.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELCLite.Identity.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.NormalizedEmail).IsRequired();
            builder.Property(x => x.PreferredLanguageISOCode).IsRequired();

            builder.Ignore(x => x.Roles);

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasIndex(x => x.NormalizedEmail);

            builder.HasKey(x => x.Id);
        }
    }
}
