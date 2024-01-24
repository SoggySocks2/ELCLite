using ELCLite.Identity.Data;
using ELCLite.Identity.Data.Contracts;
using ELCLite.Identity.Features.Roles;
using ELCLite.Identity.Features.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ELCLite.Identity.Configuration
{
    public static class IdentityServicesConfiguration
    {
        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration, IdentitySettings clientSettings)
        {
            /* Add the identity database context */
            services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(clientSettings.ConnectionString));

            /* Allow auto database migration and seeding */
            services.AddScoped<IdentityDbInitializer>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            /* Auto mapper used for mapping classes to DTO's, etc. */
            services.AddAutoMapper(typeof(IdentityServicesConfiguration).Assembly);
        }
    }
}
