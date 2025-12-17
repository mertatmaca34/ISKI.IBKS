using ISKI.IBKS.Application.Features.AnalogSensors.Dtos;
using ISKI.IBKS.Application.Features.AnalogSensors.Enums;
using ISKI.IBKS.Application.Features.AnalogSensors.Services;
using ISKI.IBKS.Domain.Abstractions;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISKI.IBKS.Infrastructure.Features.AnalogSensors;

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

        var channels = _plcSettings.Value.Station.DBs.FirstOrDefault(db => db.Type == "Analog");

        if (channels is null)
        {
            return Task.FromResult<IReadOnlyList<ChannelReadingDto>>(Array.Empty<ChannelReadingDto>());
        }

        bool isAutoMode = snapshot.KabinOtoModu ?? false;

        var list = new List<ChannelReadingDto>(capacity: channels.Offsets.Count);

        foreach (var item in channels)
        {
            var channel = item.ChannelName != null ? snapshot.GetType().GetProperty(item.ChannelName) : null;

            if (channel == null)
                continue;

            item.ChannelName = channel.Name;
            item.Value = channel.GetValue(snapshot) as double?;
            item.UnitName = "mg/L";
            item.Status = AnalogSignalEvaluator.Evaluate(isAutoMode, item.Value);
        }
        /*
        {

            new()
            {
                ChannelName = "AKM",
                Value = snapshot.Akm,
                UnitName = "mg/L",
                Status = AnalogSignalEvaluator.Evaluate(isAutoMode, snapshot.Akm)
            },
            new()
            {
                ChannelName = "pH",
                Value = snapshot.Ph,
                UnitName = "",
                Status = AnalogSignalEvaluator.Evaluate(isAutoMode, snapshot.Ph)
            },
            new()
            {
                ChannelName = "Iletkenlik",
                Value = snapshot.Iletkenlik,
                UnitName = "µS/cm",
                Status = AnalogSignalEvaluator.Evaluate(isAutoMode, snapshot.Iletkenlik)
            },
            new()
            {
                ChannelName = "Debi",
                Value = snapshot.TesisDebi,
                UnitName = "m³/h",
                Status = AnalogSignalEvaluator.Evaluate(isAutoMode, snapshot.TesisDebi)
            },
            new()
            {
                ChannelName = "DesarjDebi",
                Value = snapshot.DesarjDebi,
                UnitName = "m³/h",
                Status = AnalogSignalEvaluator.Evaluate(isAutoMode, snapshot.DesarjDebi)
            },
            new()
            {
                ChannelName = "HariciDebi",
                Value = snapshot.HariciDebi,
                UnitName = "m³/h",
                Status = AnalogSignalEvaluator.Evaluate(isAutoMode, snapshot.HariciDebi)
            },
            new()
            {
                ChannelName = "HariciDebi2",
                Value = snapshot.HariciDebi2,
                UnitName = "m³/h",
                Status = AnalogSignalEvaluator.Evaluate(isAutoMode, snapshot.HariciDebi2)
            },
            new()
            {
                ChannelName = "KOI",
                Value = snapshot.Koi,
                UnitName = "mg/L",
                Status = AnalogSignalEvaluator.Evaluate(isAutoMode, snapshot.Koi)
            },
            new()
            {
                ChannelName = "Sicaklik",
                Value = snapshot.KabinSicakligi,
                UnitName = "°C",
                Status = AnalogSignalEvaluator.Evaluate(isAutoMode, snapshot.KabinSicakligi)
            },
            new()
            {
                ChannelName = "AkisHizi",
                Value = snapshot.OlcumCihaziAkisHizi,
                UnitName = "m/s",
                Status = AnalogSignalEvaluator.Evaluate(isAutoMode, snapshot.OlcumCihaziAkisHizi)
            },
            new()
            {
                ChannelName = "CozunmusOksijen",
                Value = snapshot.CozunmusOksijen,
                UnitName = "mg/L",
                Status = AnalogSignalEvaluator.Evaluate(isAutoMode, snapshot.CozunmusOksijen)
            }
        };*/

        // Eğer snapshot'ta null gelebilen numeric alanların varsa,
        // UI'ya "boş" göndermek için filtreleyebilirsin:
        // list = list.Where(x => x.Value is not null).ToList();

        return Task.FromResult<IReadOnlyList<ChannelReadingDto>>(list);
    }
}
