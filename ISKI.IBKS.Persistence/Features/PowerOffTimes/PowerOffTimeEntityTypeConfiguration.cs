using ISKI.IBKS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISKI.IBKS.Persistence.Features.PowerOffTimes;

public class PowerOffTimeEntityTypeConfiguration : IEntityTypeConfiguration<PowerOffTime>
{
    public void Configure(EntityTypeBuilder<PowerOffTime> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.StationId).IsRequired();
        builder.Property(p => p.StartDate).IsRequired();

        builder.HasIndex(p => new { p.StationId, p.StartDate });
        builder.HasIndex(p => p.IsSentToSais);

        // Ignore computed property
        builder.Ignore(p => p.DurationMinutes);

        builder.ToTable("PowerOffTimes");
    }
}
