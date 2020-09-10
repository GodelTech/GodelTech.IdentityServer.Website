using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GodelTech.IdentityServer.Data.Contexts
{
    public class PersistedGrantStoreDbContext : PersistedGrantDbContext<PersistedGrantStoreDbContext>
    {
        public const string DefaultSchema = "id4_persist_grant";
        
        public PersistedGrantStoreDbContext(
            DbContextOptions<PersistedGrantStoreDbContext> options)
            : base(options,
                      new IdentityServer4.EntityFramework.Options.OperationalStoreOptions
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
