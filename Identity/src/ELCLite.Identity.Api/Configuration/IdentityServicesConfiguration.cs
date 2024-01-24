using ELCLite.Identity.Api.Data;
using ELCLite.Identity.Api.Data.Contracts;
using ELCLite.Identity.Api.Features.Roles;
using ELCLite.Identity.Api.Features.Users;
using ELCLite.Identity.Infrastructure.Identities;
using ELCLite.SharedKernel.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ELCLite.Identity.Api.Configuration
{
    public static class IdentityServicesConfiguration
    {
        public static void AddIdentityServices(this IServiceCollection services, IIdentitySettings identitySettings)
        {
            services.AddIdentityDbContext(identitySettings);
            services.AddIdentity();
            services.AddIdentityServices();

            /* Allow auto database migration and seeding */
            services.AddScoped<IdentityDbInitializer>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IIdentityRepository, IdentityRepository>();

            /* Auto mapper used for mapping classes to DTO's, etc. */
            services.AddAutoMapper(typeof(IdentityServicesConfiguration).Assembly);
        }

        private static void AddIdentityDbContext(this IServiceCollection services, IIdentitySettings identitySettings)
        {
            var connectionString = identitySettings.DbConnectionString ?? throw new InvalidOperationException("Connection string 'Identity - DbConnectionString' not found.");
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    if (identitySettings.DbTimeout is not null)
                    {
                        sqlServerOptions.CommandTimeout(identitySettings.DbTimeout.Value);
                    }
                })
            );
        }

        private static void AddIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityDbContext>();
            services.AddControllersWithViews();
        }


        private static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            //services.AddScoped<IIdentityService, IdentityService>();
            //services.AddScoped<IIdentityProxy, IdentityProxy>();
        }
    }
}
