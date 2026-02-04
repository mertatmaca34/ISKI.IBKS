using ISKI.IBKS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISKI.IBKS.Persistence.Features.LogEntries;

public class LogEntryEntityTypeConfiguration : IEntityTypeConfiguration<LogEntry>
{
    public void Configure(EntityTypeBuilder<LogEntry> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).ValueGeneratedOnAdd();

        builder.Property(l => l.LogTitle).IsRequired().HasMaxLength(300);
        builder.Property(l => l.LogDescription).IsRequired().HasMaxLength(2000);

        builder.HasIndex(l => new { l.StationId, l.LogCreatedDate });
        builder.HasIndex(l => l.Level);
        builder.HasIndex(l => l.Category);

        builder.ToTable("LogEntries");
    }
}
