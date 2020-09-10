using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GodelTech.IdentityServer.Data.Contexts
{
    public class ConfigurationStoreDbContext : ConfigurationDbContext<ConfigurationStoreDbContext>
    {
        public const string DefaultSchema = "id4_config";
        
        public ConfigurationStoreDbContext(
            DbContextOptions<ConfigurationStoreDbContext> options)
            : base(options,
                  new IdentityServer4.EntityFramework.Options.ConfigurationStoreOptions
                  {
                      DefaultSchema = DefaultSchema
                  }
                  )
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
