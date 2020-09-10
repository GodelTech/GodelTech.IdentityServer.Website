using System;
using System.IO;
using GodelTech.IdentityServer.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GodelTech.IdentityServer.Data.ContextFactories
{
    public class IdentityStoreDbContextFactory : IDesignTimeDbContextFactory<IdentityStoreDbContext>
    {
        public IdentityStoreDbContext CreateDbContext(string[] args)
        {
            var environmentVariable =
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Machine);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentVariable}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var configurationStoreConnection = configuration.GetConnectionString(nameof(IdentityStoreDbContext));
            var optionsBuilder = new DbContextOptionsBuilder<IdentityStoreDbContext>();
            
            optionsBuilder.UseSqlServer(
                configurationStoreConnection,
                    b => b.MigrationsAssembly("GodelTech.IdentityServer.Data"));

            return new IdentityStoreDbContext(optionsBuilder.Options);
        }
    }
}
