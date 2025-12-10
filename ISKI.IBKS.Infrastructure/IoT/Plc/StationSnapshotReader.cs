using ISKI.IBKS.Domain.Abstractions;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Infrastructure.IoT.Plc.Abstractions;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.IoT.Plc;

public class StationSnapshotReader : IStationSnapshotReader
{
    private readonly IPlcClient _plcClient;
    private readonly PlcSettings _settings;
    private readonly ILogger<StationSnapshotReader> _logger;

    public StationSnapshotReader(
    IPlcClient plcClient,
    IOptions<PlcSettings> options,
    ILogger<StationSnapshotReader> logger)
    {
        _plcClient = plcClient;
        _settings = options.Value;
        _logger = logger;
    }

    public Task<StationSnapshot?> Read(string stationIp)
    {
        StationSnapshot stationSnapshot = new StationSnapshot();

        var station = _settings.Stations.Where(s => s.IpAddress == stationIp).FirstOrDefault();

        if (_plcClient is null || station is null)
        {
            _logger.LogError("Bu {StationIp} IP adresi ile ilgili Plc konfigürasyonu bulunamadı.", stationIp);

            return Task.FromResult<StationSnapshot?>(null);
        }

        try
        {
            if (_plcClient.IsConnected == false)
            {
                _plcClient.Connect(station.IpAddress, station.Rack, station.Slot);
            }

            var db41 = station.DBs.Where(db => db.DbNumber == 41).FirstOrDefault();

            if(db41 is null)
            {
                _logger.LogError("Bu {StationIp} IP adresi ile ilgili Plc DB41 konfigürasyonu bulunamadı.", stationIp);
                return Task.FromResult<StationSnapshot?>(null);
            }

            var db41Data = _plcClient.ReadBytes(db41.DbNumber, 0, new byte[db41.Size]);

            stationSnapshot.TesisDebi = _plcClient.ReadReal(db41Data, db41.Offsets["TesisDebi"].ByteOffset);
            stationSnapshot.TesisGunlukDebi = _plcClient.ReadReal(db41Data, db41.Offsets["TesisGunlukDebi"].ByteOffset);
            stationSnapshot.DesarjDebi = _plcClient.ReadReal(db41Data, db41.Offsets["DesarjDebi"].ByteOffset);
            stationSnapshot.HariciDebi = _plcClient.ReadReal(db41Data, db41.Offsets["HariciDebi"].ByteOffset);
            stationSnapshot.HariciDebi2 = _plcClient.ReadReal(db41Data, db41.Offsets["HariciDebi2"].ByteOffset);
            stationSnapshot.OlcumCihaziAkisHizi = _plcClient.ReadReal(db41Data, db41.Offsets["OlcumCihaziAkisHizi"].ByteOffset);
            stationSnapshot.Ph = _plcClient.ReadReal(db41Data, db41.Offsets["Ph"].ByteOffset);
            stationSnapshot.Iletkenlik = _plcClient.ReadReal(db41Data, db41.Offsets["Iletkenlik"].ByteOffset);
            stationSnapshot.CozunmusOksijen = _plcClient.ReadReal(db41Data, db41.Offsets["CozunmusOksijen"].ByteOffset);
            stationSnapshot.OlcumCihaziSuSicakligi = _plcClient.ReadReal(db41Data, db41.Offsets["OlcumCihaziSuSicakligi"].ByteOffset);
            stationSnapshot.Koi = _plcClient.ReadReal(db41Data, db41.Offsets["Koi"].ByteOffset);
            stationSnapshot.Akm = _plcClient.ReadReal(db41Data, db41.Offsets["Akm"].ByteOffset);
            stationSnapshot.KabinNemi = _plcClient.ReadReal(db41Data, db41.Offsets["KabinNemi"].ByteOffset);
            stationSnapshot.KabinSicakligi = _plcClient.ReadReal(db41Data, db41.Offsets["KabinSicakligi"].ByteOffset);
            stationSnapshot.Pompa1CalismaFrekansi = _plcClient.ReadReal(db41Data, db41.Offsets["Pompa1CalismaFrekansi"].ByteOffset);
            stationSnapshot.Pompa2CalismaFrekansi = _plcClient.ReadReal(db41Data, db41.Offsets["Pompa2CalismaFrekansi"].ByteOffset);

            return Task.FromResult<StationSnapshot?>(stationSnapshot);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
