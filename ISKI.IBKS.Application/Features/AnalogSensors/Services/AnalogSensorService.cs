using ISKI.IBKS.Application.Common.Extensions;
using ISKI.IBKS.Application.Features.AnalogSensors.Abstractions;
using ISKI.IBKS.Application.Features.AnalogSensors.Dtos;
using ISKI.IBKS.Application.Features.StationSnapshots.Abstractions;
using ISKI.IBKS.Application.Features.StationSnapshots.Dtos;
using ISKI.IBKS.Application.Options;
using ISKI.IBKS.Infrastructure.Application.Features.AnalogSensors;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;
using ISKI.IBKS.Application.Common.Results;

namespace ISKI.IBKS.Application.Features.AnalogSensors.Services;

public class AnalogSensorService : IAnalogSensorService
{
    private readonly IStationSnapshotCache _stationSnapshotCache;
    private readonly UiMappingOptions _uiMapping;
    private readonly PlcSettings _plcSettings;

    public AnalogSensorService(
        IStationSnapshotCache stationSnapshotCache,
        IOptions<UiMappingOptions> uiMappingOptions,
        IOptions<PlcSettings> plcSettings)
    {
        _stationSnapshotCache = stationSnapshotCache ?? throw new ArgumentNullException(nameof(stationSnapshotCache));
        _uiMapping = uiMappingOptions?.Value ?? throw new ArgumentNullException(nameof(uiMappingOptions));
        _plcSettings = plcSettings?.Value ?? throw new ArgumentNullException(nameof(plcSettings));
    }

    public async Task<IDataResult<IReadOnlyList<ChannelReadingDto>>> GetChannelsAsync(Guid stationId, CancellationToken cancellationToken = default)
    {
        var stationSnapshot = await _stationSnapshotCache.Get(stationId);

        if (stationSnapshot == null) {
            return new ErrorDataResult<IReadOnlyList<ChannelReadingDto>>([]);
        }

        bool isAutoMode = stationSnapshot.KabinOtoModu ?? false;

        var mappings = _uiMapping.Station?.Analog ?? new List<UiMappingEntryRaw>();

        var ordered = mappings.OrderBy(m => m.Order ?? int.MaxValue).ToList();

        var list = new List<ChannelReadingDto>(ordered.Count);

        var snapshotType = typeof(StationSnapshotDto);
        var props = snapshotType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

        foreach (var map in ordered)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(map.Key))
                continue;

            if (!props.TryGetValue(map.Key, out var prop))
                continue; // not present on snapshot

            double? value = prop.GetValue(stationSnapshot).ToDouble();

            var dto = new ChannelReadingDto
            {
                ChannelId = null,
                ChannelName = map.Label ?? map.Key,
                Value = value,
                UnitName = map.Unit ?? string.Empty,
                Status = AnalogSignalEvaluator.Evaluate(isAutoMode, value),
                Format = map.Format ?? "N2"
            };

            list.Add(dto);
        }

        return new SuccesDataResult<IReadOnlyList<ChannelReadingDto>>(list);
    }
}
