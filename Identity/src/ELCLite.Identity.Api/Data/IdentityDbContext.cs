using ELCLite.Identity.Api.Data.Configuration;
using ELCLite.Identity.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELCLite.Identity.Api.Data
{
    public class IdentityDbContext : DbContext
    {
        //private readonly ICurrentUser _currentUser;

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        //public IdentityDbContext(DbContextOptions<IdentityDbContext> options, ICurrentUser currentUser) : base(options)
        //{
        //    _currentUser = currentUser;
        //}

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);

            //modelBuilder.ConfigureSoftDelete();
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // The order is important. Apply soft delete then auditing.
            //this.ApplySoftDelete();
            //this.ApplyAuditing(_currentUser);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
