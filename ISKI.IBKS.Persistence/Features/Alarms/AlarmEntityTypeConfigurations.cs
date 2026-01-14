using ISKI.IBKS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISKI.IBKS.Persistence.Features.Alarms;

public class AlarmDefinitionEntityTypeConfiguration : IEntityTypeConfiguration<AlarmDefinition>
{
    public void Configure(EntityTypeBuilder<AlarmDefinition> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.Name).IsRequired().HasMaxLength(200);
        builder.Property(a => a.SensorName).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Description).HasMaxLength(500);

        builder.HasIndex(a => a.IsActive);
        builder.HasIndex(a => a.SensorName);

        builder.ToTable("AlarmDefinitions");
    }
}

public class AlarmUserEntityTypeConfiguration : IEntityTypeConfiguration<AlarmUser>
{
    public void Configure(EntityTypeBuilder<AlarmUser> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.Property(u => u.FullName).IsRequired().HasMaxLength(200);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(256);
        builder.Property(u => u.PhoneNumber).HasMaxLength(50);
        builder.Property(u => u.Department).HasMaxLength(100);
        builder.Property(u => u.Title).HasMaxLength(100);

        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.IsActive);

        builder.ToTable("AlarmUsers");
    }
}

public class AlarmUserSubscriptionEntityTypeConfiguration : IEntityTypeConfiguration<AlarmUserSubscription>
{
    public void Configure(EntityTypeBuilder<AlarmUserSubscription> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedOnAdd();

        builder.HasIndex(s => new { s.AlarmDefinitionId, s.AlarmUserId }).IsUnique();
        builder.HasIndex(s => s.IsActive);

        builder.ToTable("AlarmUserSubscriptions");
    }
}

public class AlarmEventEntityTypeConfiguration : IEntityTypeConfiguration<AlarmEvent>
{
    public void Configure(EntityTypeBuilder<AlarmEvent> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.SensorName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Message).IsRequired().HasMaxLength(500);

        builder.HasIndex(e => new { e.StationId, e.OccurredAt });
        builder.HasIndex(e => e.ResolvedAt);
        builder.HasIndex(e => e.NotificationSent);

        builder.ToTable("AlarmEvents");
    }
}
