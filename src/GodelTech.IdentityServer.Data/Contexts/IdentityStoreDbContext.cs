using GodelTech.IdentityServer.Data.Configuration;
using GodelTech.IdentityServer.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GodelTech.IdentityServer.Data.Contexts
{
    public class IdentityStoreDbContext : IdentityDbContext<User>
    {
        public const string DefaultSchema = "identity";
        
        public IdentityStoreDbContext(DbContextOptions<IdentityStoreDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(DefaultSchema);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserSettingConfiguration());
        }
    }
}
