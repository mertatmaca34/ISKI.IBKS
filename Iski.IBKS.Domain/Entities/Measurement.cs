using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Domain.Entities
{
    internal class Measurement
    {
        public Guid Id { get; private set; }
        public Guid StationId { get; private set; }
        public DateTime Readtime { get; private set; }
        public string SoftwareVersion { get; private set; }
        public decimal AkisHizi { get; private set; }
        public decimal Akm { get; set; }
        public decimal CozunmusOksijen { get; set; }
        public decimal Debi { get; private set; }
        public decimal DesarjDebi { get; private set; }
        public decimal HariciDebi { get; private set; }
        public decimal HariciDebi2 { get; private set; }
        public decimal Koi { get; private set; }
        public decimal Ph { get; private set; }
        public decimal Sicaklik { get; private set; }
        public decimal Iletkenlik { get; private set; }
        public int Period { get; private set; }
        public MeasurementStatus Status { get; private set; }
    }

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
