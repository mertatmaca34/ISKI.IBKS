using ISKI.IBKS.Domain.IoT;
using Sharp7;

namespace ISKI.IBKS.Infrastructure.Services.DataCollection;

public static class PlcDataMapper
{
    public static void MapAnalogData(byte[] buffer, PlcDataSnapshot snapshot)
    {
        if (buffer.Length < 168) return;

        snapshot.TesisDebi = S7.GetRealAt(buffer, 0);
        snapshot.OlcumCihaziAkisHizi = S7.GetRealAt(buffer, 4);
        snapshot.NumuneDebi = S7.GetRealAt(buffer, 8);
        snapshot.TesisGunlukDebi = S7.GetRealAt(buffer, 12);
        snapshot.Ph = S7.GetRealAt(buffer, 16);
        snapshot.Iletkenlik = S7.GetRealAt(buffer, 20);
        snapshot.CozunmusOksijen = S7.GetRealAt(buffer, 24);
        snapshot.OlcumCihaziSuSicakligi = S7.GetRealAt(buffer, 28);
        snapshot.Koi = S7.GetRealAt(buffer, 32);
        snapshot.Akm = S7.GetRealAt(buffer, 36);
        snapshot.KabinSicakligi = S7.GetRealAt(buffer, 40);
        snapshot.KabinNemi = S7.GetRealAt(buffer, 44);

        snapshot.HariciDebi = S7.GetRealAt(buffer, 52);
        snapshot.HariciDebi2 = S7.GetRealAt(buffer, 56);
        snapshot.DesarjDebi = S7.GetRealAt(buffer, 60);

        snapshot.Pompa1CalismaFrekansi = S7.GetRealAt(buffer, 140);
        snapshot.Pompa2CalismaFrekansi = S7.GetRealAt(buffer, 144);
        snapshot.UpsCikisVolt = S7.GetRealAt(buffer, 148);
        snapshot.UpsGirisVolt = S7.GetRealAt(buffer, 152);
        snapshot.UpsKapasite = S7.GetRealAt(buffer, 156);
        snapshot.UpsSicaklik = S7.GetRealAt(buffer, 160);
        snapshot.UpsYuk = S7.GetRealAt(buffer, 164);
    }

    public static void MapDigitalData(byte[] buffer, PlcDataSnapshot snapshot)
    {
        if (buffer.Length < 3) return;

        snapshot.KabinOtoModu = S7.GetBitAt(buffer, 0, 0);
        snapshot.KabinBakimModu = S7.GetBitAt(buffer, 0, 1);
        snapshot.KabinKalibrasyonModu = S7.GetBitAt(buffer, 0, 2);
        snapshot.KabinDumanAlarmi = S7.GetBitAt(buffer, 0, 3);
        snapshot.KabinSuBaskiniAlarmi = S7.GetBitAt(buffer, 0, 4);
        snapshot.KabinKapiAlarmi = S7.GetBitAt(buffer, 0, 5);
        snapshot.KabinEnerjiAlarmi = S7.GetBitAt(buffer, 0, 6);
        snapshot.KabinAcilStopBasiliAlarmi = S7.GetBitAt(buffer, 0, 7);

        snapshot.KabinHaftalikYikamada = S7.GetBitAt(buffer, 1, 0);
        snapshot.KabinSaatlikYikamada = S7.GetBitAt(buffer, 1, 1);
        snapshot.Pompa1TermikAlarmi = S7.GetBitAt(buffer, 1, 2);
        snapshot.Pompa2TermikAlarmi = S7.GetBitAt(buffer, 1, 3);
        snapshot.Pompa3TermikAlarmi = S7.GetBitAt(buffer, 1, 4);
        snapshot.YikamaTankiBosAlarmi = S7.GetBitAt(buffer, 1, 5);
        snapshot.Pompa1Calisiyor = S7.GetBitAt(buffer, 1, 6);
        snapshot.Pompa2Calisiyor = S7.GetBitAt(buffer, 1, 7);

        snapshot.Pompa3Calisiyor = S7.GetBitAt(buffer, 2, 0);
        snapshot.AkmNumuneTetik = S7.GetBitAt(buffer, 2, 1);
        snapshot.KoiNumuneTetik = S7.GetBitAt(buffer, 2, 2);
        snapshot.PhNumuneTetik = S7.GetBitAt(buffer, 2, 3);
        snapshot.ManuelTetik = S7.GetBitAt(buffer, 2, 4);
        snapshot.SimNumuneTetik = S7.GetBitAt(buffer, 2, 5);
    }

    public static void MapSystemData(byte[] buffer, PlcDataSnapshot snapshot)
    {
        if (buffer.Length < 19) return;

        try
        {
            snapshot.SystemTime = S7.GetDTLAt(buffer, 0);
        }
        catch
        {
            snapshot.SystemTime = DateTime.Now;
        }

        snapshot.HaftalikYikamaGunu = S7.GetByteAt(buffer, 14);
        snapshot.HaftalikYikamaSaati = S7.GetByteAt(buffer, 15);
        snapshot.SaatlikYikamaSaati = S7.GetByteAt(buffer, 16);
        snapshot.YikamaDakikasi = S7.GetByteAt(buffer, 17);
        snapshot.YikamaSaniyesi = S7.GetByteAt(buffer, 18);
    }
}
