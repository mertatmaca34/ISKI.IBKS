using ISKI.IBKS.Application.Features.AnalogSensors.Abstractions;
using ISKI.IBKS.Application.Features.AnalogSensors.Services;
using ISKI.IBKS.Application.Features.DigitalSensors.Services;
using ISKI.IBKS.Application.Features.HealthSummary.Abstractions;
using ISKI.IBKS.Application.Features.HealthSummary.Services;
using ISKI.IBKS.Application.Features.Plc.Abstractions;
using ISKI.IBKS.Application.Features.StationSnapshots.Abstractions;
using ISKI.IBKS.Application.Features.StationStatus.Services;
using ISKI.IBKS.Application.Options;
using ISKI.IBKS.Infrastructure.Application.Features.StationStatus; // Restored
using ISKI.IBKS.Infrastructure.Services.DataCollection;
using ISKI.IBKS.Infrastructure.Services.Mail;
using ISKI.IBKS.Application.Features.Alarms;
using ISKI.IBKS.Infrastructure.Services.Alarms;
using ISKI.IBKS.Infrastructure.IoT.Plc;
using ISKI.IBKS.Infrastructure.IoT.Plc.Client.Sharp7;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using ISKI.IBKS.Infrastructure.IoT.Plc.Readers;
using ISKI.IBKS.Infrastructure.IoT.Plc.StatusProviders;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Http;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Options;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Providers;
using ISKI.IBKS.Infrastructure.BackgroundServices;
using ISKI.IBKS.Application.Services.Mail;
using ISKI.IBKS.Application.Services.DataCollection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ISKI.IBKS.Persistence;

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
        services.Configure<MailConfiguration>(configuration.GetSection("MailSettings"));
        
        // Persistence
        services.AddPersistence(configuration);

        // IoT / Plc
        services.AddSingleton<IPlcClient, Sharp7Client>();
        services.AddSingleton<IStationSnapshotReader, StationSnapshotReader>();

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
        services.AddScoped<IPlcStatusProvider, PlcStatusProvider>();
        services.AddScoped<ISaisStatusProvider, SaisStatusProvider>();
        services.AddScoped<IHealthSummaryService, HealthSummaryService>();

        // Background polling
        services.AddHostedService<PlcPollingService>();
        services.AddHostedService<DataCollectionBackgroundService>();
        
        // Logging
        services.AddSingleton<ISKI.IBKS.Infrastructure.Logging.IApplicationLogger, ISKI.IBKS.Infrastructure.Logging.ApplicationLogger>();

        // Application Services
        services.AddScoped<IAlarmMailService, ISKI.IBKS.Infrastructure.Services.Mail.SmtpAlarmMailService>();
        services.AddScoped<IAlarmManager, AlarmManager>();
        services.AddSingleton<IDataCollectionService, ISKI.IBKS.Infrastructure.Services.DataCollection.DataCollectionService>();

        return services;
    }
}