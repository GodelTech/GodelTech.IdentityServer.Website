using System;
using GodelTech.IdentityServer.Data.Contexts;
using Serilog;

namespace GodelTech.IdentityServer.Data.Migration.Seeds
{
    public static class PersistedGrantStoreDbContextSeed
    {
        public static void SeedData(this PersistedGrantStoreDbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Error occured while saving data.");
            }
        }
    }
}
