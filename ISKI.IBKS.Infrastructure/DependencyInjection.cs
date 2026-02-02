using FluentValidation;
using ISKI.IBKS.Application.Common.IoT.Plc;
using ISKI.IBKS.Infrastructure.Persistence.Contexts;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Options;
using ISKI.IBKS.Persistence;
using ISKI.IBKS.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<PlcSettings>(configuration.GetSection(ConfigurationConstants.Sections.Plc));
        services.Configure<SAISOptions>(configuration.GetSection(ConfigurationConstants.Sections.Sais));
        services.Configure<UiMappingOptions>(configuration.GetSection(ConfigurationConstants.Sections.UiMapping));
        services.Configure<Services.Mail.MailConfiguration>(configuration.GetSection(ConfigurationConstants.Sections.MailSettings));

        services.AddLocalization(options => options.ResourcesPath = "Localization");

        services.AddDbContext<IbksDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(IbksDbContext).Assembly.FullName)));

        services.AddValidatorsFromAssembly(typeof(IPlcClient).Assembly);

        services.AddSingleton<IPlcClient, IoT.Plc.Client.Sharp7.Sharp7Client>();
        services.AddSingleton<IStationSnapshotReader, IoT.Plc.Readers.StationSnapshotReader>();

        services.AddHttpClient<Application.Common.RemoteApi.SAIS.ISaisAuthClient, RemoteApi.SAIS.Http.SaisAuthClient>()
            .ConfigureHttpClient((sp, client) =>
            {
                var opts = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<SAISOptions>>().Value;
                client.BaseAddress = new Uri(opts.BaseUrl);
                client.Timeout = opts.Timeout;
            });

        services.AddSingleton<Application.Common.IoT.Snapshots.IStationSnapshotCache, IoT.Plc.StationSnapshotCache>();

        services.AddSingleton<Application.Common.RemoteApi.SAIS.ISaisTicketProvider, RemoteApi.SAIS.Providers.TicketProvider>();
        services.AddHttpClient<Application.Common.RemoteApi.SAIS.ISaisApiClient, RemoteApi.SAIS.Http.SaisApiClient>()
            .ConfigureHttpClient((sp, client) =>
            {
                var opts = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<SAISOptions>>().Value;
                client.BaseAddress = new Uri(opts.BaseUrl);
                client.Timeout = opts.Timeout;
            });

        services.AddScoped<Application.Common.Configuration.ISelectedSensorsProvider, Services.Sensors.SelectedSensorsProvider>();

        services.AddHostedService<IoT.Plc.PlcPollingService>();
        services.AddHostedService<BackgroundServices.PlcPollingWorker>();

        services.AddSingleton<Logging.IApplicationLogger, Logging.ApplicationLogger>();

        services.AddSingleton<Application.Common.Configuration.IIisDeploymentService, Services.Iis.IisDeploymentService>();
        services.AddSingleton<Application.Common.Notifications.IAlarmMailService, Services.Mail.SmtpAlarmMailService>();
        services.AddScoped<Application.Common.IoT.IDataCollectionService, Services.DataCollection.DataCollectionService>();

        services.AddSingleton<Services.DataCollection.LegacyServiceShim>();
        services.AddSingleton<Application.Common.IoT.Snapshots.IAnalogSensorService>(sp => sp.GetRequiredService<Services.DataCollection.LegacyServiceShim>());
        services.AddSingleton<Application.Common.IoT.Snapshots.IDigitalSensorService>(sp => sp.GetRequiredService<Services.DataCollection.LegacyServiceShim>());
        services.AddSingleton<Application.Common.IoT.Snapshots.IStationStatusService>(sp => sp.GetRequiredService<Services.DataCollection.LegacyServiceShim>());
        services.AddSingleton<Application.Common.IoT.Snapshots.IHealthSummaryService>(sp => sp.GetRequiredService<Services.DataCollection.LegacyServiceShim>());

        services.AddSingleton<Application.Common.Configuration.IStationConfiguration>(sp => new Services.Configuration.StationConfigurationService(configuration));
        services.AddSingleton<Application.Common.Configuration.IStationRuntimeState, Services.Configuration.StationRuntimeState>();

        return services;
    }
}
