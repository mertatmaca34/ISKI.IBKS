using ISKI.IBKS.Application.Features.Plc.Abstractions;
using ISKI.IBKS.Infrastructure.IoT.Plc.Client.Sharp7;
using ISKI.IBKS.Application.Services.DataCollection; // NEW
using Sharp7;

namespace ISKI.IBKS.Infrastructure.Services.DataCollection;

/// <summary>
/// Handles mapping of raw bytes from PLC DBs to strong typed objects.
/// Based on "ISKI.IBKS PROJESİ FULL DÖKÜMANI"
/// </summary>
public static class PlcDataMapper
{
    // DB41 - Analog Sensors (All Real/Float)
    public static void MapAnalogData(byte[] buffer, PlcDataSnapshot snapshot)
    {
        if (buffer.Length < 168) return; // Ensure buffer is large enough for max address 164 + 4 bytes

        snapshot.TesisDebi = S7.GetRealAt(buffer, 0);
        snapshot.NumuneHiz = S7.GetRealAt(buffer, 4);
        snapshot.NumuneDebi = S7.GetRealAt(buffer, 8);
        snapshot.TesisGunlukDebi = S7.GetRealAt(buffer, 12);
        snapshot.Ph = S7.GetRealAt(buffer, 16);
        snapshot.Iletkenlik = S7.GetRealAt(buffer, 20);
        snapshot.CozunmusOksijen = S7.GetRealAt(buffer, 24);
        snapshot.NumuneSicaklik = S7.GetRealAt(buffer, 28);
        snapshot.Koi = S7.GetRealAt(buffer, 32);
        snapshot.Akm = S7.GetRealAt(buffer, 36);
        snapshot.KabinSicaklik = S7.GetRealAt(buffer, 40);
        snapshot.KabinNem = S7.GetRealAt(buffer, 44);
        
        // Opsiyonel
        snapshot.HariciDebi = S7.GetRealAt(buffer, 52);
        snapshot.HariciDebi2 = S7.GetRealAt(buffer, 56);
        snapshot.DesarjDebi = S7.GetRealAt(buffer, 60);

        // System Health
        snapshot.Pompa1Hz = S7.GetRealAt(buffer, 140);
        snapshot.Pompa2Hz = S7.GetRealAt(buffer, 144);
        snapshot.UpsCikisVolt = S7.GetRealAt(buffer, 148);
        snapshot.UpsGirisVolt = S7.GetRealAt(buffer, 152);
        snapshot.UpsKapasite = S7.GetRealAt(buffer, 156);
        snapshot.UpsSicaklik = S7.GetRealAt(buffer, 160);
        snapshot.UpsYuk = S7.GetRealAt(buffer, 164);
    }

    // DB42 - Digital Sensors & Status (All Bits)
    public static void MapDigitalData(byte[] buffer, PlcDataSnapshot snapshot)
    {
        if (buffer.Length < 3) return; // Need at least 3 bytes (0, 1, 2)

        // Byte 0
        snapshot.KabinOto = S7.GetBitAt(buffer, 0, 0);
        snapshot.KabinBakim = S7.GetBitAt(buffer, 0, 1);
        snapshot.KabinKalibrasyon = S7.GetBitAt(buffer, 0, 2);
        snapshot.KabinDuman = S7.GetBitAt(buffer, 0, 3);
        snapshot.KabinSuBaskini = S7.GetBitAt(buffer, 0, 4);
        snapshot.KabinKapiAcildi = S7.GetBitAt(buffer, 0, 5);
        snapshot.KabinEnerjiYok = S7.GetBitAt(buffer, 0, 6);
        snapshot.KabinAcilStopBasili = S7.GetBitAt(buffer, 0, 7);

        // Byte 1
        snapshot.HaftalikYikamada = S7.GetBitAt(buffer, 1, 0); // Kabin_HaftalikYikamada -> HaftalikYikamada (Check definition)
        snapshot.SaatlikYikamada = S7.GetBitAt(buffer, 1, 1); // Kabin_SaatlikYikamada -> SaatlikYikamada
        snapshot.Pompa1Termik = S7.GetBitAt(buffer, 1, 2);
        snapshot.Pompa2Termik = S7.GetBitAt(buffer, 1, 3);
        snapshot.Pompa3Termik = S7.GetBitAt(buffer, 1, 4);
        snapshot.TankDolu = S7.GetBitAt(buffer, 1, 5);
        snapshot.Pompa1Calisiyor = S7.GetBitAt(buffer, 1, 6);
        snapshot.Pompa2Calisiyor = S7.GetBitAt(buffer, 1, 7);

        // Byte 2
        snapshot.Pompa3Calisiyor = S7.GetBitAt(buffer, 2, 0);
        snapshot.AkmTetik = S7.GetBitAt(buffer, 2, 1);
        snapshot.KoiTetik = S7.GetBitAt(buffer, 2, 2);
        snapshot.PhTetik = S7.GetBitAt(buffer, 2, 3);
        snapshot.ManuelTetik = S7.GetBitAt(buffer, 2, 4);
        snapshot.SimNumuneTetik = S7.GetBitAt(buffer, 2, 5);
    }

    // DB43 - System Time & Wash Timers
    public static void MapSystemData(byte[] buffer, PlcDataSnapshot snapshot)
    {
        // SystemTime starts at 0, DTL format (12 bytes)
        if (buffer.Length < 19) return;

        try 
        {
            snapshot.SystemTime = S7.GetDTLAt(buffer, 0);
        }
        catch 
        { 
            snapshot.SystemTime = DateTime.Now; // Fallback
        }

        snapshot.WeeklyWashDay = S7.GetByteAt(buffer, 14);
        snapshot.WeeklyWashHour = S7.GetByteAt(buffer, 15);
        snapshot.DailyWashHour = S7.GetByteAt(buffer, 16);
        snapshot.Minute = S7.GetByteAt(buffer, 17);
        snapshot.Second = S7.GetByteAt(buffer, 18);
    }
}
