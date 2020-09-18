using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using GodelTech.IdentityServer.Data.Contexts;
using GodelTech.IdentityServer.Data.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace GodelTech.IdentityServer.Data.Migration.Seeds
{
    public static class IdentityStoreDbContextSeed
    {
        public static void SeedData(this IdentityStoreDbContext context)
        {
            try
            {
                var services = new ServiceCollection();
                services.AddLogging();
                services.AddSingleton(typeof(IdentityStoreDbContext), context);

                services.AddAuthentication();
                services.AddAuthorization();

                var builder = services.AddIdentityCore<User>(o =>
                {
                    // configure identity options
                    o.Password.RequireDigit = false;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequiredLength = 6;
                });
                builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
                builder.AddEntityFrameworkStores<IdentityStoreDbContext>().AddDefaultTokenProviders();

                var address = new
                {
                    street_address = "One Hacker Way",
                    locality = "Heidelberg",
                    postal_code = 69118,
                    country = "Germany"
                };

                using var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var alice = userManager.FindByNameAsync("alice").GetAwaiter().GetResult();
                if (alice == null)
                {
                    alice = new User {UserName = "alice"};
                    var result = userManager.CreateAsync(alice, "Pass123$").GetAwaiter().GetResult();
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userManager.AddClaimsAsync(alice,
                        new[]
                        {
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                        })
                        .GetAwaiter().GetResult();
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    Log.Debug("alice created");
                }
                else
                {
                    Log.Debug("alice already exists");
                }

                var bob = userManager.FindByNameAsync("bob").GetAwaiter().GetResult();
                if (bob == null)
                {
                    bob = new User {UserName = "bob"};
                    var result = userManager.CreateAsync(bob, "Pass123$").GetAwaiter().GetResult();
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userManager.AddClaimsAsync(bob,
                        new[]
                        {
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                            new Claim("location", "somewhere")
                        }).GetAwaiter().GetResult();
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    Log.Debug("bob created");
                }
                else
                {
                    Log.Debug("bob already exists");
                }

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Error occured while saving data.");
            }
        }
    }
}
