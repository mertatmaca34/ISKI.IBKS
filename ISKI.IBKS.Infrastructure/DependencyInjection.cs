using ISKI.IBKS.Application.Features.AnalogSensors.Services;
using ISKI.IBKS.Domain.Abstractions;
using ISKI.IBKS.Infrastructure.AnalogSensors;
using ISKI.IBKS.Infrastructure.IoT.Plc;
using ISKI.IBKS.Infrastructure.IoT.Plc.Abstractions;
using ISKI.IBKS.Infrastructure.IoT.Plc.Client.Sharp7;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Http;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Options;
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

        services.Configure<SAISOptions>(configuration.GetSection("SAIS"));

        // Cache
        services.AddSingleton<IStationSnapshotCache, StationSnapshotCache>();

        //saisApi
        services.AddHttpClient<ISaisApiClient, SaisApiClient>((sp, client) =>
        {
            var opts = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<SAISOptions>>().Value;

            client.BaseAddress = new Uri(opts.BaseUrl);
            client.Timeout = opts.Timeout;
        });

        //services
        services.AddScoped<IAnalogSensorService, AnalogSensorService>();

        // Background polling
        services.AddHostedService<PlcPollingService>();

        // ileride DbContext, HttpClient vs. de burada

        return services;
    }
}
