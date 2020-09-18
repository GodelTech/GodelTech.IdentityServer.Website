using GodelTech.IdentityServer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GodelTech.IdentityServer.Data.Configuration
{
    internal class UserSettingConfiguration : BaseDomainEntityConfiguration<UserSetting>, IEntityTypeConfiguration<UserSetting>
    {
        public new void Configure(EntityTypeBuilder<UserSetting> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("UserSettings");

            builder.Property(entity => entity.Name)
                .IsRequired()
                .HasMaxLength(128);
            
            builder.Property(entity => entity.Value)
                .HasMaxLength(256);
            
            builder.Property(entity => entity.Type)
                .IsRequired()
                .HasMaxLength(128);

            builder
                .HasOne(setting => setting.SettingOwner)
                .WithMany(user => user.Settings);
        }
    }
}
