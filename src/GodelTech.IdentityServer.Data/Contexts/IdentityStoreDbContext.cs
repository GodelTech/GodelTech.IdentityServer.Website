using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GodelTech.IdentityServer.Data.Contexts
{
    public class IdentityStoreDbContext : IdentityDbContext<IdentityUser>
    {
        public const string DefaultSchema = "identity";
        
        public IdentityStoreDbContext(DbContextOptions<IdentityStoreDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(DefaultSchema);
        }
    }
}
