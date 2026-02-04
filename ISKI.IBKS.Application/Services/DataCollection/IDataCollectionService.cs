namespace ISKI.IBKS.Application.Services.DataCollection;

/// <summary>
/// PLC veri toplama servisi arayüzü - Dakikalık veri okuma ve kaydetme
/// </summary>
public interface IDataCollectionService
{
    /// <summary>
    /// PLC'den anlık veri okur
    /// </summary>
    Task<PlcDataSnapshot?> ReadCurrentDataAsync(CancellationToken ct = default);

    /// <summary>
    /// Enerji kesintisi kontrolü yapar
    /// </summary>
    Task CheckPowerOffAsync(CancellationToken ct = default);

    /// <summary>
    /// Kalibrasyon sonucunu kaydeder ve SAIS'e bildirir
    /// </summary>
    Task SaveAndSendCalibrationAsync(Domain.Entities.Calibration calibration, CancellationToken ct = default);

    /// <summary>
    /// İstasyon ID'sini döndürür
    /// </summary>
    Task<Guid> GetStationIdAsync(CancellationToken ct = default);

    /// <summary>
    /// İstasyon ayarlarını döndürür
    /// </summary>
    Task<Domain.Entities.StationSettings?> GetStationSettingsAsync(CancellationToken ct = default);

    /// <summary>
    /// Okunan veriyi veritabanına kaydeder
    /// </summary>
    Task<Domain.Entities.SensorData?> SaveSensorDataAsync(PlcDataSnapshot data, CancellationToken ct = default);

    /// <summary>
    /// Veriyi SAIS API'ye gönderir
    /// </summary>
    Task<bool> SendDataToSaisAsync(PlcDataSnapshot data, CancellationToken ct = default);

    /// <summary>
    /// Verinin gönderildiğini işaretler
    /// </summary>
    Task MarkDataAsSentAsync(Guid sensorDataId, CancellationToken ct = default);

    /// <summary>
    /// Alarmları kontrol eder ve gerekirse bildirim gönderir
    /// </summary>
    Task CheckAndTriggerAlarmsAsync(PlcDataSnapshot data, CancellationToken ct = default);

    /// <summary>
    /// SAIS'ten gelen istek üzerine numune alımını başlatır (PLC'ye yazar)
    /// </summary>
    Task<bool> StartSampleAsync(string sampleCode, CancellationToken ct = default);
}

/// <summary>
/// PLC'den okunan anlık veri paketi
/// </summary>
public record PlcDataSnapshot
{
    public DateTime ReadTime { get; init; } = DateTime.Now;
    public Guid StationId { get; init; }

    // Analog Sensörler (DB41)
    public double TesisDebi { get; set; }
    public double TesisGunlukDebi { get; set; }
    public double NumuneHiz { get; set; }
    public double NumuneDebi { get; set; }
    public double Ph { get; set; }
    public double Iletkenlik { get; set; }
    public double CozunmusOksijen { get; set; }
    public double NumuneSicaklik { get; set; }
    public double Koi { get; set; }
    public double Akm { get; set; }
    public double KabinSicaklik { get; set; }
    public double KabinNem { get; set; }
    public double DesarjDebi { get; set; }
    public double HariciDebi { get; set; }
    public double HariciDebi2 { get; set; }
    public double Pompa1Hz { get; set; }
    public double Pompa2Hz { get; set; }
    public double UpsCikisVolt { get; set; }
    public double UpsGirisVolt { get; set; }
    public double UpsKapasite { get; set; }
    public double UpsSicaklik { get; set; }
    public double UpsYuk { get; set; }

    // Dijital Sensörler (DB42)
    public bool KabinOto { get; set; }
    public bool KabinBakim { get; set; }
    public bool KabinKalibrasyon { get; set; }
    public bool KabinDuman { get; set; }
    public bool KabinSuBaskini { get; set; }
    public bool KabinKapiAcildi { get; set; }
    public bool KabinEnerjiYok { get; set; }
    public bool KabinAcilStopBasili { get; set; }
    public bool HaftalikYikamada { get; set; }
    public bool SaatlikYikamada { get; set; }
    public bool Pompa1Termik { get; set; }
    public bool Pompa2Termik { get; set; }
    public bool Pompa3Termik { get; set; }
    public bool TankDolu { get; set; }
    public bool Pompa1Calisiyor { get; set; }
    public bool Pompa2Calisiyor { get; set; }
    public bool Pompa3Calisiyor { get; set; }
    public bool AkmTetik { get; set; }
    public bool KoiTetik { get; set; }
    public bool PhTetik { get; set; }
    public bool ManuelTetik { get; set; }
    public bool SimNumuneTetik { get; set; }

    // Sistem Verileri (DB43)
    public DateTime SystemTime { get; set; }
    public byte WeeklyWashDay { get; set; }
    public byte WeeklyWashHour { get; set; }
    public byte DailyWashHour { get; set; }
    public byte Minute { get; set; }
    public byte Second { get; set; }

    /// <summary>
    /// Veri durumu (status) - SAIS için
    /// </summary>
    public int GetDataStatus()
    {
        // 1 = VeriGecerli
        if (KabinEnerjiYok) return 0; // VeriYok
        if (KabinKalibrasyon) return 9; // SistemKal
        if (SaatlikYikamada) return 23; // Yikama
        if (HaftalikYikamada) return 24; // HaftalikYikama
        if (KabinBakim) return 25; // IstasyonBakimda
        return 1; // VeriGecerli
    }
}
