using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ISKI.IBKS.Application.Common.IoT.Plc;
using ISKI.IBKS.Domain.IoT;
using Microsoft.Extensions.Logging;
using Sharp7;

namespace ISKI.IBKS.Infrastructure.IoT.Plc.Readers;

public sealed class StationSnapshotReader : IStationSnapshotReader
{
    private readonly IPlcClient _plcClient;
    private readonly ILogger<StationSnapshotReader> _logger;
    private const int MaxRetryAttempts = 3;

    public StationSnapshotReader(IPlcClient plcClient, ILogger<StationSnapshotReader> logger)
    {
        _plcClient = plcClient;
        _logger = logger;
    }

    public async Task<PlcDataSnapshot> ReadAsync(PlcStationConfig station, CancellationToken ct = default)
    {
        Exception? lastException = null;

        for (int attempt = 1; attempt <= MaxRetryAttempts; attempt++)
        {
            if (ct.IsCancellationRequested) break;

            try
            {
                return await ReadInternalAsync(station, ct);
            }
            catch (Exception ex)
            {
                lastException = ex;
                _logger.LogWarning(ex, "PLC okuma denemesi {Attempt}/{MaxAttempts} başarısız: {StationIp}",
                    attempt, MaxRetryAttempts, station.IpAddress);

                if (attempt < MaxRetryAttempts)
                {
                    await Task.Delay(500 * attempt, ct);
                    await _plcClient.DisconnectAsync(ct);
                }
            }
        }

        _logger.LogError(lastException, "PLC okuma {MaxAttempts} deneme sonrasında başarısız oldu", MaxRetryAttempts);
        throw lastException!;
    }

    private async Task<PlcDataSnapshot> ReadInternalAsync(PlcStationConfig station, CancellationToken ct)
    {
        PlcDataSnapshot stationSnapshot = new PlcDataSnapshot();

        if (!_plcClient.IsConnected)
        {
            bool connected = await _plcClient.ConnectAsync(station.IpAddress, station.Rack, station.Slot, ct);
            if (!connected) throw new Exception("PLC connection failed");
        }

        var db41 = GetDb(station, 41);
        var db41Data = await _plcClient.ReadBytesAsync(db41.DbNumber, 0, db41.Size, ct);

        stationSnapshot.TesisDebi = S7.GetRealAt(db41Data, db41.Offsets["TesisDebi"].ByteOffset);
        stationSnapshot.TesisGunlukDebi = S7.GetRealAt(db41Data, db41.Offsets["TesisGunlukDebi"].ByteOffset);
        stationSnapshot.DesarjDebi = S7.GetRealAt(db41Data, db41.Offsets["DesarjDebi"].ByteOffset);
        stationSnapshot.HariciDebi = S7.GetRealAt(db41Data, db41.Offsets["HariciDebi"].ByteOffset);
        stationSnapshot.HariciDebi2 = S7.GetRealAt(db41Data, db41.Offsets["HariciDebi2"].ByteOffset);
        stationSnapshot.OlcumCihaziAkisHizi = S7.GetRealAt(db41Data, db41.Offsets["OlcumCihaziAkisHizi"].ByteOffset);
        stationSnapshot.Ph = S7.GetRealAt(db41Data, db41.Offsets["Ph"].ByteOffset);
        stationSnapshot.Iletkenlik = S7.GetRealAt(db41Data, db41.Offsets["Iletkenlik"].ByteOffset);
        stationSnapshot.CozunmusOksijen = S7.GetRealAt(db41Data, db41.Offsets["CozunmusOksijen"].ByteOffset);
        stationSnapshot.OlcumCihaziSuSicakligi = S7.GetRealAt(db41Data, db41.Offsets["OlcumCihaziSuSicakligi"].ByteOffset);
        stationSnapshot.Koi = S7.GetRealAt(db41Data, db41.Offsets["Koi"].ByteOffset);
        stationSnapshot.Akm = S7.GetRealAt(db41Data, db41.Offsets["Akm"].ByteOffset);
        stationSnapshot.KabinNemi = S7.GetRealAt(db41Data, db41.Offsets["KabinNemi"].ByteOffset);
        stationSnapshot.KabinSicakligi = S7.GetRealAt(db41Data, db41.Offsets["KabinSicakligi"].ByteOffset);
        stationSnapshot.Pompa1CalismaFrekansi = S7.GetRealAt(db41Data, db41.Offsets["Pompa1CalismaFrekansi"].ByteOffset);
        stationSnapshot.Pompa2CalismaFrekansi = S7.GetRealAt(db41Data, db41.Offsets["Pompa2CalismaFrekansi"].ByteOffset);

        var db42 = GetDb(station, 42);
        var db42Data = await _plcClient.ReadBytesAsync(db42.DbNumber, 0, db42.Size, ct);

        stationSnapshot.KabinOtoModu = S7.GetBitAt(db42Data, db42.Offsets["KabinOtoModu"].ByteOffset, db42.Offsets["KabinOtoModu"].BitOffset!.Value);
        stationSnapshot.KabinBakimModu = S7.GetBitAt(db42Data, db42.Offsets["KabinBakimModu"].ByteOffset, db42.Offsets["KabinBakimModu"].BitOffset!.Value);
        stationSnapshot.KabinKalibrasyonModu = S7.GetBitAt(db42Data, db42.Offsets["KabinKalibrasyonModu"].ByteOffset, db42.Offsets["KabinKalibrasyonModu"].BitOffset!.Value);
        stationSnapshot.KabinDumanAlarmi = S7.GetBitAt(db42Data, db42.Offsets["KabinDumanAlarmi"].ByteOffset, db42.Offsets["KabinDumanAlarmi"].BitOffset!.Value);
        stationSnapshot.KabinSuBaskiniAlarmi = S7.GetBitAt(db42Data, db42.Offsets["KabinSuBaskiniAlarmi"].ByteOffset, db42.Offsets["KabinSuBaskiniAlarmi"].BitOffset!.Value);
        stationSnapshot.KabinKapiAlarmi = S7.GetBitAt(db42Data, db42.Offsets["KabinKapiAlarmi"].ByteOffset, db42.Offsets["KabinKapiAlarmi"].BitOffset!.Value);
        stationSnapshot.KabinEnerjiAlarmi = S7.GetBitAt(db42Data, db42.Offsets["KabinEnerjiAlarmi"].ByteOffset, db42.Offsets["KabinEnerjiAlarmi"].BitOffset!.Value);
        stationSnapshot.KabinAcilStopBasiliAlarmi = S7.GetBitAt(db42Data, db42.Offsets["KabinAcilStopBasiliAlarmi"].ByteOffset, db42.Offsets["KabinAcilStopBasiliAlarmi"].BitOffset!.Value);
        stationSnapshot.KabinHaftalikYikamada = S7.GetBitAt(db42Data, db42.Offsets["KabinHaftalikYikamada"].ByteOffset, db42.Offsets["KabinHaftalikYikamada"].BitOffset!.Value);
        stationSnapshot.KabinSaatlikYikamada = S7.GetBitAt(db42Data, db42.Offsets["KabinSaatlikYikamada"].ByteOffset, db42.Offsets["KabinSaatlikYikamada"].BitOffset!.Value);
        stationSnapshot.Pompa1TermikAlarmi = S7.GetBitAt(db42Data, db42.Offsets["Pompa1TermikAlarmi"].ByteOffset, db42.Offsets["Pompa1TermikAlarmi"].BitOffset!.Value);
        stationSnapshot.Pompa2TermikAlarmi = S7.GetBitAt(db42Data, db42.Offsets["Pompa2TermikAlarmi"].ByteOffset, db42.Offsets["Pompa2TermikAlarmi"].BitOffset!.Value);
        stationSnapshot.Pompa3TermikAlarmi = S7.GetBitAt(db42Data, db42.Offsets["Pompa3TermikAlarmi"].ByteOffset, db42.Offsets["Pompa3TermikAlarmi"].BitOffset!.Value);
        stationSnapshot.YikamaTankiBosAlarmi = S7.GetBitAt(db42Data, db42.Offsets["YikamaTankiBosAlarmi"].ByteOffset, db42.Offsets["YikamaTankiBosAlarmi"].BitOffset!.Value);
        stationSnapshot.Pompa1Calisiyor = S7.GetBitAt(db42Data, db42.Offsets["Pompa1Calisiyor"].ByteOffset, db42.Offsets["Pompa1Calisiyor"].BitOffset!.Value);
        stationSnapshot.Pompa2Calisiyor = S7.GetBitAt(db42Data, db42.Offsets["Pompa2Calisiyor"].ByteOffset, db42.Offsets["Pompa2Calisiyor"].BitOffset!.Value);
        stationSnapshot.Pompa3Calisiyor = S7.GetBitAt(db42Data, db42.Offsets["Pompa3Calisiyor"].ByteOffset, db42.Offsets["Pompa3Calisiyor"].BitOffset!.Value);
        stationSnapshot.AkmNumuneTetik = S7.GetBitAt(db42Data, db42.Offsets["AkmNumuneTetik"].ByteOffset, db42.Offsets["AkmNumuneTetik"].BitOffset!.Value);
        stationSnapshot.KoiNumuneTetik = S7.GetBitAt(db42Data, db42.Offsets["KoiNumuneTetik"].ByteOffset, db42.Offsets["KoiNumuneTetik"].BitOffset!.Value);
        stationSnapshot.PhNumuneTetik = S7.GetBitAt(db42Data, db42.Offsets["PhNumuneTetik"].ByteOffset, db42.Offsets["PhNumuneTetik"].BitOffset!.Value);
        stationSnapshot.ManuelTetik = S7.GetBitAt(db42Data, db42.Offsets["ManuelTetik"].ByteOffset, db42.Offsets["ManuelTetik"].BitOffset!.Value);
        stationSnapshot.SimNumuneTetik = S7.GetBitAt(db42Data, db42.Offsets["SimNumuneTetik"].ByteOffset, db42.Offsets["SimNumuneTetik"].BitOffset!.Value);

        var db43 = GetDb(station, 43);
        var db43Data = await _plcClient.ReadBytesAsync(db43.DbNumber, 0, db43.Size, ct);
        stationSnapshot.SystemTime = S7.GetDTLAt(db43Data, db43.Offsets["SystemTime"].ByteOffset);
        stationSnapshot.HaftalikYikamaGunu = S7.GetByteAt(db43Data, db43.Offsets["HaftalikYikamaGunu"].ByteOffset);
        stationSnapshot.HaftalikYikamaSaati = S7.GetByteAt(db43Data, db43.Offsets["HaftalikYikamaSaati"].ByteOffset);
        stationSnapshot.SaatlikYikamaSaati = S7.GetByteAt(db43Data, db43.Offsets["SaatlikYikamaSaati"].ByteOffset);
        stationSnapshot.YikamaDakikasi = S7.GetByteAt(db43Data, db43.Offsets["YikamaDakikasi"].ByteOffset);
        stationSnapshot.YikamaSaniyesi = S7.GetByteAt(db43Data, db43.Offsets["YikamaSaniyesi"].ByteOffset);

        return stationSnapshot;
    }

    private PlcDbConfig GetDb(PlcStationConfig station, int dbNumber)
    {
        var db = station.DBs.FirstOrDefault(x => x.DbNumber == dbNumber);
        if (db == null)
        {
            throw new InvalidOperationException($"DB{dbNumber} configuration not found for station {station.IpAddress}");
        }
        return db;
    }
}
