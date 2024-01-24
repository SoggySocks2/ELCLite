﻿using ELCLite.Identity.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELCLite.Identity.Data.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable(nameof(UserRole));

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasKey(x => new { x.UserId, x.RoleId });
        }
    }
}
