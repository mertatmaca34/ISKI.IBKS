using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ISKI.IBKS.Persistence.Contexts;

public sealed class IbksDbContext : DbContext
{
    public IbksDbContext(DbContextOptions<IbksDbContext> options) : base(options)
    {
    }

    // Existing
    public DbSet<Calibration> Calibrations => Set<Calibration>();

    // Sensor Data
    public DbSet<SensorData> SensorDatas => Set<SensorData>();

    // Alarm Management
    public DbSet<AlarmDefinition> AlarmDefinitions => Set<AlarmDefinition>();
    public DbSet<AlarmUser> AlarmUsers => Set<AlarmUser>();
    public DbSet<AlarmUserSubscription> AlarmUserSubscriptions => Set<AlarmUserSubscription>();
    public DbSet<AlarmEvent> AlarmEvents => Set<AlarmEvent>();

    // Logging and Tracking
    public DbSet<LogEntry> LogEntries => Set<LogEntry>();
    public DbSet<PowerOffTime> PowerOffTimes => Set<PowerOffTime>();
    public DbSet<SampleRequest> SampleRequests => Set<SampleRequest>();

    // Station and Channel Configuration
    public DbSet<StationSettings> StationSettings => Set<StationSettings>();
    public DbSet<ChannelInformation> ChannelInformations => Set<ChannelInformation>();
    public DbSet<DiagnosticType> DiagnosticTypes => Set<DiagnosticType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IbksDbContext).Assembly);
        modelBuilder.ApplySoftDeleteQueryFilter();
        base.OnModelCreating(modelBuilder);
    }
}
