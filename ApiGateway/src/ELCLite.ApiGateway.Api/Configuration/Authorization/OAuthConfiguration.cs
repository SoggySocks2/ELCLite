using ELCLite.SharedKernel.SharedObjects;
using Microsoft.AspNetCore.Authorization;

namespace ELCLite.ApiGateway.Api.Configuration.Authorization
{
    public static class OAuthConfiguration
    {
        public const string ScopeAccessAsUser = "access_as_user";


        //public static void AddB2CAuthentication(this IServiceCollection services, IConfiguration configuration)
        //{
        //    //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    //    .AddMicrosoftIdentityWebApi(options =>
        //    //    {
        //    //        configuration.Bind("AzureAdB2C", options);

        //    //        //options.TokenValidationParameters.NameClaimType = "name";
        //    //    },
        //    //    options => { configuration.Bind("AzureAdB2C", options); })
        //    //    ;

        //    //services.AddAuthentication(x =>
        //    //{
        //    //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    //});

        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultScheme = "Bearer";
        //        options.DefaultChallengeScheme = "Bearer";
        //    })
        //    .AddJwtBearer("Bearer", options =>
        //    {
        //        options.Authority = "https://localhost:44380";
        //        options.Audience = "client.mydomain.com";
        //        options.RequireHttpsMetadata = true;
        //        options.SaveToken = true;
        //        options.IncludeErrorDetails = true;
        //    });
        //    //.AddJwtBearer("bearer", null);
        //}

        public static void AddPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                //options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireScope(ScopeAccessAsUser).Build();
                //options.AddPolicy(ScopeAccessAsUser, policy => policy.RequireScope(ScopeAccessAsUser));

                foreach (var permissionKey in PermissionKey.List)
                {
                    options.AddPolicy(permissionKey.Name, policy => policy.RequirePermission(permissionKey.Value));
                }
            });
        }
    }
}
