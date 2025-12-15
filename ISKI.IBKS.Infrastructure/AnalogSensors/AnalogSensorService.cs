using ISKI.IBKS.Application.Features.AnalogSensors.Dtos;
using ISKI.IBKS.Application.Features.AnalogSensors.Services;
using ISKI.IBKS.Domain.Abstractions;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.AnalogSensors;

public class AnalogSensorService : IAnalogSensorService
{
    private readonly IStationSnapshotCache _stationSnapshotCache;
    private readonly IOptions<PlcSettings> _plcSettings;

    public AnalogSensorService(IStationSnapshotCache stationSnapshotCache, IOptions<PlcSettings> plcSettings)
    {
        _stationSnapshotCache = stationSnapshotCache;
        _plcSettings = plcSettings;
    }

    public Task<IReadOnlyList<ChannelReadingDto>> GetChannelsAsync(
        Guid stationId,
        CancellationToken cancellationToken = default)
    {
        // cancellationToken burada sync işte kullanılmıyor ama interface standardı diye dursun.
        if (!_stationSnapshotCache.TryGet(stationId, out var snapshot) || snapshot is null)
        {
            return Task.FromResult<IReadOnlyList<ChannelReadingDto>>(Array.Empty<ChannelReadingDto>());
        }

        // İstersen burada plcSettings üzerinden station var mı kontrol edebilirsin.
        // var station = _plcSettings.Value.Stations.FirstOrDefault(x => x.StationCode == stationId);

        // Snapshot -> DTO mapping (reflection YOK)
        var list = new List<ChannelReadingDto>(capacity: 16)
        {
            new()
            {
                ChannelName = "AKM",
                Value = snapshot.Akm,
                UnitName = "mg/L",
                Status = 1
            },
            new()
            {
                ChannelName = "pH",
                Value = snapshot.Ph,
                UnitName = "",
                Status = 1
            },
            new()
            {
                ChannelName = "Iletkenlik",
                Value = snapshot.Iletkenlik,
                UnitName = "µS/cm",
                Status = 1
            },
            new()
            {
                ChannelName = "Debi",
                Value = snapshot.TesisDebi,
                UnitName = "m³/h",
                Status = 1
            },
            new()
            {
                ChannelName = "DesarjDebi",
                Value = snapshot.DesarjDebi,
                UnitName = "m³/h",
                Status = 1
            },
            new()
            {
                ChannelName = "HariciDebi",
                Value = snapshot.HariciDebi,
                UnitName = "m³/h",
                Status = 1
            },
            new()
            {
                ChannelName = "HariciDebi2",
                Value = snapshot.HariciDebi2,
                UnitName = "m³/h",
                Status = 1
            },
            new()
            {
                ChannelName = "KOI",
                Value = snapshot.Koi,
                UnitName = "mg/L",
                Status = 1
            },
            new()
            {
                ChannelName = "Sicaklik",
                Value = snapshot.KabinSicakligi,
                UnitName = "°C",
                Status = 1
            },
            new()
            {
                ChannelName = "AkisHizi",
                Value = snapshot.OlcumCihaziAkisHizi,
                UnitName = "m/s",
                Status = 1
            },
            new()
            {
                ChannelName = "CozunmusOksijen",
                Value = snapshot.CozunmusOksijen,
                UnitName = "mg/L",
                Status = 1
            }
        };

        // Eğer snapshot'ta null gelebilen numeric alanların varsa,
        // UI'ya "boş" göndermek için filtreleyebilirsin:
        // list = list.Where(x => x.Value is not null).ToList();

        return Task.FromResult<IReadOnlyList<ChannelReadingDto>>(list);
    }
}
