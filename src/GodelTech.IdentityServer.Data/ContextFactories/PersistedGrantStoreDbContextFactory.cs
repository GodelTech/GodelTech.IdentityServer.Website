using System;
using System.IO;
using GodelTech.IdentityServer.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GodelTech.IdentityServer.Data.ContextFactories
{
    public class PersistedGrantStoreDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantStoreDbContext>
    {
        public PersistedGrantStoreDbContext CreateDbContext(string[] args)
        {
            var environmentVariable =
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Machine);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentVariable}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var configurationStoreConnection = configuration.GetConnectionString(nameof(PersistedGrantStoreDbContext));
            var optionsBuilder = new DbContextOptionsBuilder<PersistedGrantStoreDbContext>();
            
            optionsBuilder.UseSqlServer(
                configurationStoreConnection,
                    b => b.MigrationsAssembly("GodelTech.IdentityServer.Data"));

            return new PersistedGrantStoreDbContext(optionsBuilder.Options);
        }
    }
}
