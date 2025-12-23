using ISKI.IBKS.Application.Features.StationSnapshots.Abstractions;
using ISKI.IBKS.Application.Features.StationSnapshots.Dtos;
using ISKI.IBKS.Infrastructure.IoT.Plc.Abstractions;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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

    public Task<StationSnapshotDto?> Read(string stationIp)
    {
        StationSnapshotDto stationSnapshot = new StationSnapshotDto();

        var station = _settings.Station;

        if (_plcClient is null || station is null)
        {
            _logger.LogError("Bu {StationIp} IP adresi ile ilgili Plc konfigürasyonu bulunamadı.", stationIp);

            return Task.FromResult<StationSnapshotDto?>(null);
        }

        try
        {
            if (_plcClient.IsConnected == false)
            {
                _plcClient.Connect(station.IpAddress, station.Rack, station.Slot);
            }

            var db41 = station.DBs.Where(db => db.DbNumber == 41).FirstOrDefault();

            if (db41 is null)
            {
                _logger.LogError("Bu {StationIp} IP adresi ile ilgili Plc DB41 konfigürasyonu bulunamadı.", stationIp);
                return Task.FromResult<StationSnapshotDto?>(null);
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

            var db42 = station.DBs.Where(db => db.DbNumber == 42).FirstOrDefault();

            if (db42 is null)
            {
                _logger.LogError("Bu {StationIp} IP adresi ile ilgili Plc DB42 konfigürasyonu bulunamadı.", stationIp);
                return Task.FromResult<StationSnapshotDto?>(null);
            }

            var db42Data = _plcClient.ReadBytes(db42.DbNumber, 0, new byte[db42.Size]);

            stationSnapshot.KabinOtoModu = _plcClient.ReadBit(db42Data, db42.Offsets["KabinOtoModu"].ByteOffset, db42.Offsets["KabinOtoModu"].BitOffset!.Value);
            stationSnapshot.KabinBakimModu = _plcClient.ReadBit(db42Data, db42.Offsets["KabinBakimModu"].ByteOffset, db42.Offsets["KabinBakimModu"].BitOffset!.Value);
            stationSnapshot.KabinKalibrasyonModu = _plcClient.ReadBit(db42Data, db42.Offsets["KabinKalibrasyonModu"].ByteOffset, db42.Offsets["KabinKalibrasyonModu"].BitOffset!.Value);
            stationSnapshot.KabinDumanAlarmi = _plcClient.ReadBit(db42Data, db42.Offsets["KabinDumanAlarmi"].ByteOffset, db42.Offsets["KabinDumanAlarmi"].BitOffset!.Value);
            stationSnapshot.KabinSuBaskiniAlarmi = _plcClient.ReadBit(db42Data, db42.Offsets["KabinSuBaskiniAlarmi"].ByteOffset, db42.Offsets["KabinSuBaskiniAlarmi"].BitOffset!.Value);
            stationSnapshot.KabinKapiAlarmi = _plcClient.ReadBit(db42Data, db42.Offsets["KabinKapiAlarmi"].ByteOffset, db42.Offsets["KabinKapiAlarmi"].BitOffset!.Value);
            stationSnapshot.KabinEnerjiAlarmi = _plcClient.ReadBit(db42Data, db42.Offsets["KabinEnerjiAlarmi"].ByteOffset, db42.Offsets["KabinEnerjiAlarmi"].BitOffset!.Value);
            stationSnapshot.KabinAcilStopBasiliAlarmi = _plcClient.ReadBit(db42Data, db42.Offsets["KabinAcilStopBasiliAlarmi"].ByteOffset, db42.Offsets["KabinAcilStopBasiliAlarmi"].BitOffset!.Value);
            stationSnapshot.KabinHaftalikYikamada = _plcClient.ReadBit(db42Data, db42.Offsets["KabinHaftalikYikamada"].ByteOffset, db42.Offsets["KabinHaftalikYikamada"].BitOffset!.Value);
            stationSnapshot.KabinSaatlikYikamada = _plcClient.ReadBit(db42Data, db42.Offsets["KabinSaatlikYikamada"].ByteOffset, db42.Offsets["KabinSaatlikYikamada"].BitOffset!.Value);
            stationSnapshot.Pompa1TermikAlarmi = _plcClient.ReadBit(db42Data, db42.Offsets["Pompa1TermikAlarmi"].ByteOffset, db42.Offsets["Pompa1TermikAlarmi"].BitOffset!.Value);
            stationSnapshot.Pompa2TermikAlarmi = _plcClient.ReadBit(db42Data, db42.Offsets["Pompa2TermikAlarmi"].ByteOffset, db42.Offsets["Pompa2TermikAlarmi"].BitOffset!.Value);
            stationSnapshot.Pompa3TermikAlarmi = _plcClient.ReadBit(db42Data, db42.Offsets["Pompa3TermikAlarmi"].ByteOffset, db42.Offsets["Pompa3TermikAlarmi"].BitOffset!.Value);
            stationSnapshot.YikamaTankiBosAlarmi = _plcClient.ReadBit(db42Data, db42.Offsets["YikamaTankiBosAlarmi"].ByteOffset, db42.Offsets["YikamaTankiBosAlarmi"].BitOffset!.Value);
            stationSnapshot.Pompa1Calisiyor = _plcClient.ReadBit(db42Data, db42.Offsets["Pompa1Calisiyor"].ByteOffset, db42.Offsets["Pompa1Calisiyor"].BitOffset!.Value);
            stationSnapshot.Pompa2Calisiyor = _plcClient.ReadBit(db42Data, db42.Offsets["Pompa2Calisiyor"].ByteOffset, db42.Offsets["Pompa2Calisiyor"].BitOffset!.Value);
            stationSnapshot.Pompa3Calisiyor = _plcClient.ReadBit(db42Data, db42.Offsets["Pompa3Calisiyor"].ByteOffset, db42.Offsets["Pompa3Calisiyor"].BitOffset!.Value);
            stationSnapshot.AkmNumuneTetik = _plcClient.ReadBit(db42Data, db42.Offsets["AkmNumuneTetik"].ByteOffset, db42.Offsets["AkmNumuneTetik"].BitOffset!.Value);
            stationSnapshot.KoiNumuneTetik = _plcClient.ReadBit(db42Data, db42.Offsets["KoiNumuneTetik"].ByteOffset, db42.Offsets["KoiNumuneTetik"].BitOffset!.Value);
            stationSnapshot.PhNumuneTetik = _plcClient.ReadBit(db42Data, db42.Offsets["PhNumuneTetik"].ByteOffset, db42.Offsets["PhNumuneTetik"].BitOffset!.Value);
            stationSnapshot.ManuelTetik = _plcClient.ReadBit(db42Data, db42.Offsets["ManuelTetik"].ByteOffset, db42.Offsets["ManuelTetik"].BitOffset!.Value);
            stationSnapshot.SimNumuneTetik = _plcClient.ReadBit(db42Data, db42.Offsets["SimNumuneTetik"].ByteOffset, db42.Offsets["SimNumuneTetik"].BitOffset!.Value);

            var db43 = station.DBs.Where(db => db.DbNumber == 43).FirstOrDefault();

            if (db43 is null)
            {
                _logger.LogError("Bu {StationIp} IP adresi ile ilgili Plc DB43 konfigürasyonu bulunamadı.", stationIp);
                return Task.FromResult<StationSnapshotDto?>(null);
            }

            var db43Data = _plcClient.ReadBytes(db43.DbNumber, 0, new byte[db43.Size]);
            stationSnapshot.SystemTime = _plcClient.ReadDateTime(db43Data, db43.Offsets["SystemTime"].ByteOffset);
            stationSnapshot.HaftalikYikamaGunu = _plcClient.ReadByte(db43Data, db43.Offsets["HaftalikYikamaGunu"].ByteOffset);
            stationSnapshot.HaftalikYikamaSaati = _plcClient.ReadByte(db43Data, db43.Offsets["HaftalikYikamaSaati"].ByteOffset);
            stationSnapshot.SaatlikYikamaSaati = _plcClient.ReadByte(db43Data, db43.Offsets["SaatlikYikamaSaati"].ByteOffset);
            stationSnapshot.YikamaDakikasi = _plcClient.ReadByte(db43Data, db43.Offsets["YikamaDakikasi"].ByteOffset);
            stationSnapshot.YikamaSaniyesi = _plcClient.ReadByte(db43Data, db43.Offsets["YikamaSaniyesi"].ByteOffset);

            return Task.FromResult<StationSnapshotDto?>(stationSnapshot);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
