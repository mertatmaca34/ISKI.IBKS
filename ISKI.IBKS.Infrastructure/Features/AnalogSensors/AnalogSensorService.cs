using ISKI.IBKS.Application.Features.AnalogSensors.Dtos;
using ISKI.IBKS.Application.Features.AnalogSensors.Enums;
using ISKI.IBKS.Application.Features.AnalogSensors.Services;
using ISKI.IBKS.Application.Features.StationSnapshots.Abstractions;
using ISKI.IBKS.Application.Features.StationSnapshots.Dtos;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Infrastructure.Configuration;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.Features.AnalogSensors;

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

    public Task<IReadOnlyList<ChannelReadingDto>> GetChannelsAsync(Guid stationId, CancellationToken cancellationToken = default)
    {
        if (!_stationSnapshotCache.TryGet(stationId, out var snapshot) || snapshot is null)
        {
            return Task.FromResult<IReadOnlyList<ChannelReadingDto>>(Array.Empty<ChannelReadingDto>());
        }

        bool isAutoMode = snapshot.KabinOtoModu ?? false;

        // Use only mappings declared in ui-mapping -> Station -> Analog
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

            var raw = prop.GetValue(snapshot);

            double? value = raw switch
            {
                float f => (double)f,
                double d => d,
                int i => (double)i,
                long l => (double)l,
                decimal m => (double)m,
                null => null,
                _ => TryParseToDouble(raw)
            };

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

        return Task.FromResult<IReadOnlyList<ChannelReadingDto>>(list);

        static double? TryParseToDouble(object obj)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch
            {
                return null;
            }
        }
    }
}
