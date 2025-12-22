using ISKI.IBKS.Application.Features;
using ISKI.IBKS.Application.Features.AnalogSensors.Services;
using ISKI.IBKS.Application.Features.StationStatus.Services;
using ISKI.IBKS.Application.Features.DigitalSensors.Services;
using ISKI.IBKS.Application.Features.HealthSummary.Services;
using ISKI.IBKS.Domain.Abstractions;
using ISKI.IBKS.Infrastructure.Configuration;
using ISKI.IBKS.Infrastructure.Features.AnalogSensors;
using ISKI.IBKS.Infrastructure.Features.DigitalSensors;
using ISKI.IBKS.Infrastructure.Features.HealthSummary;
using ISKI.IBKS.Infrastructure.Features.StationStatus;
using ISKI.IBKS.Infrastructure.IoT.Plc;
using ISKI.IBKS.Infrastructure.IoT.Plc.Abstractions;
using ISKI.IBKS.Infrastructure.IoT.Plc.Client.Sharp7;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using ISKI.IBKS.Infrastructure.IoT.Plc.Readers;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Http;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Options;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ISKI.IBKS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Bind PLC, SAIS and UI mapping configs from provided IConfiguration (plc.json, sais.json, ui-mapping.json)
        services.Configure<PlcSettings>(configuration.GetSection("Plc"));
        services.Configure<SAISOptions>(configuration.GetSection("SAIS"));
        services.Configure<UiMappingOptions>(configuration.GetSection("UiMapping"));

        // IoT / Plc
        services.AddSingleton<IPlcClient, Sharp7Client>();
        services.AddSingleton<IStationSnapshotReader, StationSnapshotReader>();
        services.AddSingleton<IStationPlcTagReader, StationPlcTagReader>();

        // IoC: SAIS auth client (no ticket header) - typed HttpClient
        services.AddHttpClient<ISaisAuthClient, SaisAuthClient>()
            .ConfigureHttpClient((sp, client) =>
            {
                var opts = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<SAISOptions>>().Value;
                client.BaseAddress = new Uri(opts.BaseUrl);
                client.Timeout = opts.Timeout;
            });

        // Cache
        services.AddSingleton<IStationSnapshotCache, StationSnapshotCache>();

        //saisApi
        // Register TicketProvider as singleton that resolves ISaisAuthClient from scope at call time to avoid DI cycles
        services.AddSingleton<ISaisTicketProvider, TicketProvider>();

        services.AddHttpClient<ISaisApiClient, SaisApiClient>()
            .ConfigureHttpClient((sp, client) =>
            {
                var opts = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<SAISOptions>>().Value;
                client.BaseAddress = new Uri(opts.BaseUrl);
                client.Timeout = opts.Timeout;
            });

        //services
        services.AddScoped<IAnalogSensorService, AnalogSensorService>();
        services.AddScoped<IStationStatusService, StationStatusService>();
        services.AddScoped<IDigitalSensorService, DigitalSensorService>();
        services.AddScoped<IHealthSummaryService, HealthSummaryService>();

        // Background polling
        services.AddHostedService<PlcPollingService>();
        //services.AddHostedService<SaisTicketKeepAliveService>();

        return services;
    }
}
