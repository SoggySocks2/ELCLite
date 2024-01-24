using ELCLite.ApiGateway.Api;
using ELCLite.ApiGateway.Api.Configuration.Authorization;
using ELCLite.ApiGateway.Api.Configuration.Swagger;
using ELCLite.ApiGateway.Api.Features.Identity.Contracts;
using ELCLite.ApiGateway.Api.Features.Identity.Services;
using ELCLite.Identity.Configuration;
using ELCLite.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Bind(IdentitySettings.CONFIG_NAME, IdentitySettings.Instance);
//builder.Configuration.Bind(ClientSettings.CONFIG_NAME, ClientSettings.Instance);

builder.Services.AddJwtAuthentication(builder.Configuration, IdentitySettings.Instance);
builder.Services.AddPolicies();

builder.Services.AddIdentityServices(builder.Configuration, IdentitySettings.Instance);

builder.Services.AddSingleton(IdentitySettings.Instance);
//builder.Services.AddSingleton<IClientSettings>(services => ClientSettings.Instance);

builder.Services.AddApiGatewayServices();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddControllers(o =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        //.RequireScope(OAuthConfiguration.ScopeAccessAsUser)
        .Build();
    o.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddSwaggerConfiguration();


var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCustomSwagger();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

await app.InitialiseIdentity();

app.Run();



namespace ELCLite.ApiGateway.Api
{
    public static class ApiGatewayConfiguration
    {
        public static void AddApiGatewayServices(this IServiceCollection services)
        {
            services.AddSingleton<IPermissionValidator>(services => PermissionValidator.Instance);

            // Identity feature
            services.AddScoped<IUserProxy, UserProxy>();
            services.AddScoped<IRoleProxy, RoleProxy>();
        }

        public static async Task InitialiseIdentity(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var environment = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();

                if (environment.IsDevelopment())
                {
                    var identityDbInitializer = scope.ServiceProvider.GetRequiredService<IdentityDbInitializer>();
                    await identityDbInitializer.Seed();
                }

                var permissionValidator = scope.ServiceProvider.GetRequiredService<IPermissionValidator>();
                var roleProxy = scope.ServiceProvider.GetRequiredService<IRoleProxy>();
                var roles = await roleProxy.GetWithPermissionsAsync(CancellationToken.None);
                permissionValidator.UpdateRoles(roles);
            }
        }
    }
}