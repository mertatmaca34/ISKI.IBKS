using ISKI.IBKS.Application.Common.Configuration;

namespace ISKI.IBKS.Infrastructure.Services.Sensors;

public sealed class SelectedSensorsProvider : ISelectedSensorsProvider
{
    private readonly IStationConfiguration _stationConfig;

    public SelectedSensorsProvider(IStationConfiguration stationConfig)
    {
        _stationConfig = stationConfig;
    }

    public Task<IReadOnlyList<string>> GetSelectedSensorsAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_stationConfig.SelectedSensors);
    }
}

