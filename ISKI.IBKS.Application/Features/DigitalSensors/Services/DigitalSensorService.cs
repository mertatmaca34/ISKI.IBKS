using ISKI.IBKS.Application.Common.Results;
using ISKI.IBKS.Application.Features.AnalogSensors.Dtos;
using ISKI.IBKS.Application.Features.DigitalSensors.Dtos;
using ISKI.IBKS.Application.Features.StationSnapshots.Abstractions;
using ISKI.IBKS.Application.Features.StationSnapshots.Dtos;
using ISKI.IBKS.Application.Options;
using ISKI.IBKS.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Features.DigitalSensors.Services;

public class DigitalSensorService : IDigitalSensorService
{
    private readonly IStationSnapshotCache _stationSnapshotCache;
    private readonly UiMappingOptions _uiMapping;

    public DigitalSensorService(IStationSnapshotCache stationSnapshotCache, IOptions<UiMappingOptions> uiMappingOptions)
    {
        _stationSnapshotCache = stationSnapshotCache ?? throw new ArgumentNullException(nameof(stationSnapshotCache));
        _uiMapping = uiMappingOptions?.Value ?? throw new ArgumentNullException(nameof(uiMappingOptions));
    }

    public async Task<IDataResult<IReadOnlyList<DigitalReadingDto>>> GetDigitalSensorsAsync(Guid stationId, CancellationToken cancellationToken = default)
    {
        var stationSnapshot = await _stationSnapshotCache.Get(stationId);

        if (stationSnapshot == null)
        {
            return new ErrorDataResult<IReadOnlyList<DigitalReadingDto>>([]);
        }

        var mappings = _uiMapping.Station?.Digital /* temporary - will change; but ui-mapping.json currently has Station->Analog */ ?? new List<UiMappingEntryRaw>();

        // We'll read Digital section from ui-mapping.json. Adjust UiMappingOptions if necessary.
        var digitalMappings = (_uiMapping.GetType().GetProperty("Station")?.GetValue(_uiMapping) as dynamic)?.Digital as IEnumerable<dynamic>;

        if (digitalMappings is null)
            return new ErrorDataResult<IReadOnlyList<DigitalReadingDto>>([]);

        var ordered = digitalMappings.OrderBy(m => (int?)m.Order ?? int.MaxValue).ToList();

        var snapshotType = typeof(StationSnapshotDto);
        var props = snapshotType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

        var list = new List<DigitalReadingDto>(ordered.Count);

        foreach (var dm in ordered)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string key = dm.Key as string ?? string.Empty;
            string label = dm.Label as string ?? key;

            if (string.IsNullOrWhiteSpace(key))
                continue;

            if (!props.TryGetValue(key, out var prop))
                continue;

            var val = prop.GetValue(stationSnapshot) as bool?;

            list.Add(new DigitalReadingDto
            {
                Key = key,
                Label = label,
                Value = val
            });
        }

        return new SuccesDataResult<IReadOnlyList<DigitalReadingDto>>(list);
    }
}
