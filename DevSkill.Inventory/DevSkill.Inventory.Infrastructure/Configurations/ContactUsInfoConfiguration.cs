using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    public class ContactUsInfoConfiguration : IEntityTypeConfiguration<ContactUsInfo>
    {
        public void Configure(EntityTypeBuilder<ContactUsInfo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Address).HasMaxLength(512);
            builder.Property(x => x.Phone).HasMaxLength(64);
            builder.Property(x => x.Email).HasMaxLength(128);
            builder.Property(x => x.OpenHours).HasMaxLength(128);
            builder.Property(x => x.MapEmbedUrl).HasMaxLength(1024);
            builder.Property(x => x.HeaderTitle).HasMaxLength(128);
            builder.Property(x => x.HeaderSubtitle).HasMaxLength(256);
            builder.Property(x => x.HeaderDescription).HasMaxLength(512);
        }
    }
}
