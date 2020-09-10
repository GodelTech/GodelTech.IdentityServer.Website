using GodelTech.IdentityServer.Data.ContextFactories;
using GodelTech.IdentityServer.Data.Migration.Seeds;
using Microsoft.EntityFrameworkCore;

namespace GodelTech.IdentityServer.Data.Migration.Managers
{
    public class PersistedGrantStoreDbContextMigrationManager : IMigrationManager
    {
        public void Migrate(string[] args, bool withDataSeed = false)
        {
            var dbContextFactory = new PersistedGrantStoreDbContextFactory();

            using var context = dbContextFactory.CreateDbContext(args);
            context.Database.Migrate();

            if (withDataSeed)
            {
                context.SeedData();
            }
        }
    }
}
