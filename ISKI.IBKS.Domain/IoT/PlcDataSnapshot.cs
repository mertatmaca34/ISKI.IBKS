namespace ISKI.IBKS.Domain.IoT;

public sealed class PlcDataSnapshot
{
    public DateTime ReadTime { get; set; }
    public Guid StationId { get; set; }

    public double TesisDebi { get; set; }
    public double OlcumCihaziAkisHizi { get; set; }
    public double NumuneDebi { get; set; }
    public double TesisGunlukDebi { get; set; }
    public double Ph { get; set; }
    public double Iletkenlik { get; set; }
    public double CozunmusOksijen { get; set; }
    public double OlcumCihaziSuSicakligi { get; set; }
    public double Koi { get; set; }
    public double Akm { get; set; }
    public double KabinSicakligi { get; set; }
    public double KabinNemi { get; set; }
    public double HariciDebi { get; set; }
    public double HariciDebi2 { get; set; }
    public double DesarjDebi { get; set; }

    public double Pompa1CalismaFrekansi { get; set; }
    public double Pompa2CalismaFrekansi { get; set; }
    public double UpsCikisVolt { get; set; }
    public double UpsGirisVolt { get; set; }
    public double UpsKapasite { get; set; }
    public double UpsSicaklik { get; set; }
    public double UpsYuk { get; set; }

    public bool KabinOtoModu { get; set; }
    public bool KabinBakimModu { get; set; }
    public bool KabinKalibrasyonModu { get; set; }
    public bool KabinDumanAlarmi { get; set; }
    public bool KabinSuBaskiniAlarmi { get; set; }
    public bool KabinKapiAlarmi { get; set; }
    public bool KabinEnerjiAlarmi { get; set; }
    public bool KabinAcilStopBasiliAlarmi { get; set; }
    public bool KabinHaftalikYikamada { get; set; }
    public bool KabinSaatlikYikamada { get; set; }
    public bool Pompa1TermikAlarmi { get; set; }
    public bool Pompa2TermikAlarmi { get; set; }
    public bool Pompa3TermikAlarmi { get; set; }
    public bool YikamaTankiBosAlarmi { get; set; }
    public bool Pompa1Calisiyor { get; set; }
    public bool Pompa2Calisiyor { get; set; }
    public bool Pompa3Calisiyor { get; set; }
    public bool AkmNumuneTetik { get; set; }
    public bool KoiNumuneTetik { get; set; }
    public bool PhNumuneTetik { get; set; }
    public bool ManuelTetik { get; set; }
    public bool SimNumuneTetik { get; set; }

    public DateTime SystemTime { get; set; }
    public int HaftalikYikamaGunu { get; set; }
    public int HaftalikYikamaSaati { get; set; }
    public int SaatlikYikamaSaati { get; set; }
    public int YikamaDakikasi { get; set; }
    public int YikamaSaniyesi { get; set; }
}
