using ISKI.IBKS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISKI.IBKS.Persistence.Features.SensorDatas;

public class SensorDataEntityTypeConfiguration : IEntityTypeConfiguration<SensorData>
{
    public void Configure(EntityTypeBuilder<SensorData> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedOnAdd();

        builder.Property(s => s.StationId).IsRequired();
        builder.Property(s => s.ReadTime).IsRequired();
        builder.Property(s => s.Period).IsRequired();

        // Index for efficient querying by station and time
        builder.HasIndex(s => new { s.StationId, s.ReadTime });
        builder.HasIndex(s => s.IsSentToSais);

        builder.ToTable("SensorDatas");
    }
}
