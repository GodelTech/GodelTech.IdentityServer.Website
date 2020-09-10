using System;
using System.IO;
using System.Linq;
using GodelTech.IdentityServer.Data.Migration.Managers;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace GodelTech.IdentityServer.Data.Migration
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
                    .CreateLogger();

                var environmentVariable =
                    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Machine);

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{environmentVariable}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();


                Console.WriteLine("Wait...");

                var migrationManagers = new IMigrationManager[]
                {
                     new ConfigurationStoreDbContextMigrationManager(),
                     new PersistedGrantStoreDbContextMigrationManager(),
                     new IdentityStoreDbContextMigrationManager()
                };

                foreach (var migrationManager in migrationManagers)
                {
                    migrationManager.Migrate(args, args.Any(arg => arg.ToLowerInvariant().Equals("seed")) || configuration.GetValue<bool>("Config:SeedData"));
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");

                Environment.Exit(100);
            }
            finally
            {
                Log.CloseAndFlush();

                Environment.Exit(0);
            }
        }
    }
}
