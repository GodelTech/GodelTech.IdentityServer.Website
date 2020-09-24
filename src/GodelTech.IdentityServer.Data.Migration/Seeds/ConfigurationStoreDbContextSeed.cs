using System;
using System.Linq;
using GodelTech.IdentityServer.Data.Contexts;
using IdentityModel;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Serilog;
using ApiResource = IdentityServer4.Models.ApiResource;
using ApiScope = IdentityServer4.Models.ApiScope;
using Client = IdentityServer4.Models.Client;
using Secret = IdentityServer4.Models.Secret;

namespace GodelTech.IdentityServer.Data.Migration.Seeds
{
    public static class ConfigurationStoreDbContextSeed
    {
        public static void SeedData(this ConfigurationStoreDbContext context)
        {
            try
            {
                if (!context.IdentityResources.Any())
                {
                    context.IdentityResources.AddRange(
                        new IdentityResources.OpenId().ToEntity(),
                        new IdentityResources.Profile().ToEntity(),
                        new IdentityResources.Email().ToEntity(),
                        new IdentityResources.Phone().ToEntity(),
                        new IdentityResources.Address().ToEntity()
                    );
                }

                if (!context.ApiScopes.Any())
                {
                    context.ApiScopes.AddRange(
                        new ApiScope("sample-api-scope")
                        {
                            ShowInDiscoveryDocument = true,
                            UserClaims = new[]
                            {
                                JwtClaimTypes.Name, JwtClaimTypes.EmailVerified, JwtClaimTypes.FamilyName,
                                JwtClaimTypes.Email, JwtClaimTypes.GivenName
                            }
                        }.ToEntity()
                    );
                }

                if (!context.ApiResources.Any())
                {
                    context.ApiResources.Add(new ApiResource("sample-api", "My API #1")
                    {
                        Scopes = new[] {"sample-api-scope"}
                    }.ToEntity());
                }

                if (!context.Clients.Any())
                {
                    context.Clients.AddRange(
                        // m2m client credentials flow client
                        new Client
                        {
                            ClientId = "m2m.client",
                            ClientName = "Client Credentials Client",
                            AllowedGrantTypes = GrantTypes.ClientCredentials,
                            ClientSecrets = {new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256())},
                            AllowedScopes = {"scope1"}
                        }.ToEntity(),
                        // interactive client using code flow + pkce
                        new Client
                        {
                            ClientId = "interactive",
                            ClientSecrets = {new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256())},
                            AllowedGrantTypes = GrantTypes.Code,
                            RedirectUris = {"https://localhost:44300/signin-oidc"},
                            FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                            PostLogoutRedirectUris = {"https://localhost:44300/signout-callback-oidc"},
                            AllowOfflineAccess = true,
                            AllowedScopes = {"openid", "profile", "scope2"}
                        }.ToEntity(),
                        new Client
                        {
                            ClientId = "client",
                            ClientName = "Client Credentials Client",
                            AllowedGrantTypes = GrantTypes.ClientCredentials,
                            ClientSecrets = {new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256())},
                            AllowedScopes = {"api1"}
                        }.ToEntity(),
                        new Client
                        {
                            ClientId = "spa",
                            ClientName = "SPA Client",
                            ClientUri = "https://localhost:5001",
                            AllowedGrantTypes = GrantTypes.Code,
                            RequirePkce = true,
                            RequireClientSecret = false,
                            RedirectUris =
                            {
                                "https://localhost:5001/index.html",
                                "https://localhost:5001/callback.html",
                                "https://localhost:5001/silent.html",
                                "https://localhost:5001/popup.html",
                                "https://localhost:5001/login-callback",
                                "https://localhost:5001/login-callback.html",
                                "https://localhost:5001/silent-refresh.html"
                            },
                            PostLogoutRedirectUris = {"https://localhost:5001/index.html"},
                            AllowedCorsOrigins = {"https://localhost:5001"},
                            AllowedScopes =
                            {
                                "openid",
                                "profile",
                                "email",
                                "address",
                                "phone"
                            }
                        }.ToEntity(),
                        new Client
                        {
                            ClientId = "mvc",
                            ClientName = "MVC Client",
                            AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                            RequirePkce = true,
                            ClientSecrets = {new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256())},
                            RedirectUris = {"http://localhost:5003/signin-oidc"},
                            FrontChannelLogoutUri = "http://localhost:5003/signout-oidc",
                            PostLogoutRedirectUris = {"http://localhost:5003/signout-callback-oidc"},
                            AllowOfflineAccess = true,
                            AllowedScopes = {"openid", "profile", "api1"}
                        }.ToEntity()
                    );
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
