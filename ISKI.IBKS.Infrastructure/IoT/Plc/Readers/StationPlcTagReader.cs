using ISKI.IBKS.Infrastructure.IoT.Plc.Abstractions;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using ISKI.IBKS.Infrastructure.IoT.Plc.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Xml;

namespace ISKI.IBKS.Infrastructure.IoT.Plc.Readers;

public sealed class StationPlcTagReader : IStationPlcTagReader
{
    private readonly IPlcClient _plcClient;              // sende neyse: Sharp7 wrapper vs.
    private readonly IOptions<PlcSettings> _options;
    private readonly ILogger<StationPlcTagReader> _logger;

    public StationPlcTagReader(
        IPlcClient plcClient,
        IOptions<PlcSettings> options,
        ILogger<StationPlcTagReader> logger)
    {
        _plcClient = plcClient;
        _options = options;
        _logger = logger;
    }

    public TagBag ReadTagBag(Guid stationId, CancellationToken ct = default)
    {
        var bag = new TagBag();

        var station = _options.Value.Station;
        if (station.StationId != stationId)
        {
            // ileride multi-station olacaksa burada station lookup yaparsın
            _logger.LogWarning("Station config mismatch. Requested: {StationId}, Config: {ConfigStationId}", stationId, station.StationId);
            return bag;
        }

        // DB bazında oku, sonra offsetleri decode et
        foreach (var db in station.DBs)
        {
            ct.ThrowIfCancellationRequested();

            byte[] buffer = new byte[db.Size];

            try
            {
                buffer = _plcClient.ReadBytes(db.DbNumber, db.Size, new byte[db.Size]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PLC DB read failed. DbNumber: {DbNumber}", db.DbNumber);
                continue;
            }

            foreach (var kv in db.Offsets)
            {
                var key = kv.Key;
                var def = kv.Value;

                try
                {
                    object? value = def.DataType switch
                    {
                        "Real" => _plcClient.ReadReal(buffer, def.ByteOffset),
                        "Bool" => _plcClient.ReadBit(buffer, def.ByteOffset, def.BitOffset ?? 0),
                        "Byte" => _plcClient.ReadByte(buffer, def.ByteOffset),
                        "DateTime" => _plcClient.ReadDateTime(buffer, def.ByteOffset),
                        _ => null
                    };

                    bag.Set(key, value);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Decode failed for tag {Key} in DB{DbNumber}", key, db.DbNumber);
                    bag.Set<object?>(key, null);
                }
            }
        }

        return bag;
    }
}
