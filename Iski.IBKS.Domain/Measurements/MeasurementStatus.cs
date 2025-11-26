using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iski.IBKS.Domain.Measurements
{
    public enum MeasurementStatus
    {
        VeriYok = 0,
        VeriGecerli = 1,
        Gecersiz = 4,
        Nokta1Kalibrasyon = 35,
        Nokta2Kalibrasyon = 36,
        KalLimitDisi = 7,
        IletisimHatasi = 8,
        SistemKal = 9,
        Alarm = 12,
        Purge = 15,
        KalHatasi = 19,
        AkisYok = 21,
        DesarjYok = 22,
        Yikama = 23,
        HaftalikYikama = 24,
        IstasyonBakimda = 25,
        TesisBakimda = 26,
        OlcumAraligiDisinda = 39,
        CihazBakimda = 30,
        DebiArizasi = 31,
        GecersizYikama = 200,
        GecersizHaftalikYikama = 201,
        GecersizKalibrasyon = 202,
        GecersizAkisHizi = 203,
        GecersizDebi = 204,
        TekrarVeri = 205,
        GecersizOlcumBirimi = 206
    }
}
