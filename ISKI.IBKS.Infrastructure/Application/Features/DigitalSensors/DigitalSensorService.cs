using ISKI.IBKS.Application.Features.DigitalSensors.Dtos;
using ISKI.IBKS.Application.Features.DigitalSensors.Services;
using ISKI.IBKS.Application.Features.StationSnapshots.Abstractions;
using ISKI.IBKS.Application.Features.StationSnapshots.Dtos;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.Application.Features.DigitalSensors;

public class DigitalSensorService : IDigitalSensorService
{
    private readonly IStationSnapshotCache _stationSnapshotCache;
    private readonly UiMappingOptions _uiMapping;

    public DigitalSensorService(IStationSnapshotCache stationSnapshotCache, IOptions<UiMappingOptions> uiMappingOptions)
    {
        _stationSnapshotCache = stationSnapshotCache ?? throw new ArgumentNullException(nameof(stationSnapshotCache));
        _uiMapping = uiMappingOptions?.Value ?? throw new ArgumentNullException(nameof(uiMappingOptions));
    }

    public Task<IReadOnlyList<DigitalReadingDto>> GetDigitalSensorsAsync(Guid stationId, CancellationToken cancellationToken = default)
    {
        if (!_stationSnapshotCache.TryGet(stationId, out var snapshot) || snapshot is null)
            return Task.FromResult<IReadOnlyList<DigitalReadingDto>>(Array.Empty<DigitalReadingDto>());


        var mappings = _uiMapping.Station?.Digital /* temporary - will change; but ui-mapping.json currently has Station->Analog */ ?? new List<UiMappingEntryRaw>();

        // We'll read Digital section from ui-mapping.json. Adjust UiMappingOptions if necessary.
        var digitalMappings = (_uiMapping.GetType().GetProperty("Station")?.GetValue(_uiMapping) as dynamic)?.Digital as IEnumerable<dynamic>;

        if (digitalMappings is null)
            return Task.FromResult<IReadOnlyList<DigitalReadingDto>>(Array.Empty<DigitalReadingDto>());

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

            var val = prop.GetValue(snapshot) as bool?;

            list.Add(new DigitalReadingDto
            {
                Key = key,
                Label = label,
                Value = val
            });
        }

        return Task.FromResult<IReadOnlyList<DigitalReadingDto>>(list);
    }
}
