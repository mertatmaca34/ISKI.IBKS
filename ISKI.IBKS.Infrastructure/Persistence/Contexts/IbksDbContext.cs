using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Domain.Enums;
using ISKI.IBKS.Infrastructure.Persistence.Extensions;
using ISKI.IBKS.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace ISKI.IBKS.Infrastructure.Persistence.Contexts;

public sealed class IbksDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new UpdateAuditableEntitiesInterceptor());
        base.OnConfiguring(optionsBuilder);
    }

    public IbksDbContext(DbContextOptions<IbksDbContext> options) : base(options)
    {
    }

    public DbSet<Calibration> Calibrations => Set<Calibration>();

    public DbSet<SensorData> SensorDatas => Set<SensorData>();

    public DbSet<AlarmDefinition> AlarmDefinitions => Set<AlarmDefinition>();
    public DbSet<AlarmUser> AlarmUsers => Set<AlarmUser>();
    public DbSet<AlarmUserSubscription> AlarmUserSubscriptions => Set<AlarmUserSubscription>();
    public DbSet<AlarmEvent> AlarmEvents => Set<AlarmEvent>();

    public DbSet<LogEntry> LogEntries => Set<LogEntry>();
    public DbSet<PowerOffTime> PowerOffTimes => Set<PowerOffTime>();
    public DbSet<SampleRequest> SampleRequests => Set<SampleRequest>();

    public DbSet<ChannelInformation> ChannelInformations => Set<ChannelInformation>();
    public DbSet<DiagnosticType> DiagnosticTypes => Set<DiagnosticType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IbksDbContext).Assembly);
        modelBuilder.ApplySoftDeleteQueryFilter();
        base.OnModelCreating(modelBuilder);
    }
}

