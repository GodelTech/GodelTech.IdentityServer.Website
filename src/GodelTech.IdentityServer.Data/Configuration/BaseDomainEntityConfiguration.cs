using GodelTech.IdentityServer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GodelTech.IdentityServer.Data.Configuration
{
    internal abstract class BaseDomainEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity: BaseDomainEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id).UseIdentityColumn();

            builder.Property(e => e.CreatedOn)
                .HasColumnType("Date")
                .HasDefaultValueSql("GetDate()");

            builder.Property(e => e.ModifiedOn)
                .HasColumnType("Date")
                .HasDefaultValueSql("GetDate()");
        }
    }
}
