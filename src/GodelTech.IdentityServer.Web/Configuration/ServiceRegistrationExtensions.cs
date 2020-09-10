using GodelTech.IdentityServer.Data.Contexts;
using IdentityServer4;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GodelTech.IdentityServer.Web.Configuration
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddIdentityServerDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityStoreDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(nameof(IdentityStoreDbContext))));
            
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                 // configure identity options
                 options.Password.RequireDigit = true;
                 options.Password.RequireLowercase = true;
                 options.Password.RequireUppercase = true;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<IdentityStoreDbContext>()
            .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                
                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
            .AddOperationalStore<PersistedGrantStoreDbContext>(options =>
            {
                options.ConfigureDbContext = builder => builder
                    .UseSqlServer(configuration.GetConnectionString(nameof(PersistedGrantStoreDbContext)));
                options.EnableTokenCleanup = true;
                options.DefaultSchema = PersistedGrantStoreDbContext.DefaultSchema;
            })
            .AddConfigurationStore<ConfigurationStoreDbContext>(options =>
            {
                options.ConfigureDbContext = builder => builder
                    .UseSqlServer(configuration.GetConnectionString(nameof(ConfigurationStoreDbContext)));
                options.DefaultSchema = ConfigurationStoreDbContext.DefaultSchema;
            })
            .AddAspNetIdentity<IdentityUser>()
            // not recommended for production - you need to store your key material somewhere secure
            .AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to https://localhost:5001/signin-google
                    options.ClientId = "copy client ID from Google here";
                    options.ClientSecret = "copy client secret from Google here";
                });
        }
    }
}
