using ISKI.IBKS.Domain.Abstractions;
using ISKI.IBKS.Infrastructure.IoT.Plc;
using ISKI.IBKS.Infrastructure.IoT.Plc.Abstractions;
using ISKI.IBKS.Infrastructure.IoT.Plc.Client.Sharp7;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // PlcSettings binding BURADA
        services.Configure<PlcSettings>(configuration.GetSection("Plc"));

        // IoT / Plc
        services.AddSingleton<IPlcClient, Sharp7Client>();
        services.AddSingleton<IStationSnapshotReader, StationSnapshotReader>();

        // Cache
        //services.AddSingleton<IStationSnapshotCache, InMemoryStationSnapshotCache>();

        // Background polling
        //services.AddHostedService<PlcPollingService>();

        // ileride DbContext, HttpClient vs. de burada

        return services;
    }
}
