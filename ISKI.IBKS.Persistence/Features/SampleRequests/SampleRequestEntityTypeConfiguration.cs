using ISKI.IBKS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISKI.IBKS.Persistence.Features.SampleRequests;

public class SampleRequestEntityTypeConfiguration : IEntityTypeConfiguration<SampleRequest>
{
    public void Configure(EntityTypeBuilder<SampleRequest> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedOnAdd();

        builder.Property(s => s.StationId).IsRequired();
        builder.Property(s => s.SampleCode).HasMaxLength(50);
        builder.Property(s => s.TriggerParameter).HasMaxLength(100);
        builder.Property(s => s.ErrorMessage).HasMaxLength(500);

        builder.HasIndex(s => new { s.StationId, s.StartedAt });
        builder.HasIndex(s => s.SampleCode);
        builder.HasIndex(s => s.Status);

        builder.ToTable("SampleRequests");
    }
}
