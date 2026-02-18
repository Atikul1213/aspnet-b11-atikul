using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    public class LogTableConfiguration : IEntityTypeConfiguration<LogTable>
    {
        public void Configure(EntityTypeBuilder<LogTable> builder)
        {
            builder.ToTable("Logs");

            builder.Property(l => l.Id)
                  .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(l => l.Timestamp)
                   .IsRequired();

            builder.Property(l => l.Level)
                   .IsRequired()
                   .HasMaxLength(128);

            builder.Property(l => l.Message)
                   .IsRequired();

            builder.Property(l => l.Exception)
                   .HasColumnType("nvarchar(max)");

            builder.Property(l => l.Properties)
                   .HasColumnType("nvarchar(max)");

            builder.Property(l => l.MachineName)
                   .HasMaxLength(256);

            builder.Property(l => l.ThreadId)
                   .HasMaxLength(256);
        }
    }
}
