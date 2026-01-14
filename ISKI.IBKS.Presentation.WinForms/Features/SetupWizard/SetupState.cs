namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard;

/// <summary>
/// Kurulum durumunu temsil eder
/// </summary>
public class SetupState
{
    // PLC Ayarları
    public string PlcIpAddress { get; set; } = "10.33.3.253";
    public int PlcRack { get; set; } = 0;
    public int PlcSlot { get; set; } = 1;
    public List<string> SelectedSensors { get; set; } = new();

    // SAIS API Ayarları
    public string SaisApiUrl { get; set; } = "https://entegrationsais.csb.gov.tr/";
    public string SaisUserName { get; set; } = string.Empty;
    public string SaisPassword { get; set; } = string.Empty;

    // İstasyon Ayarları
    public Guid StationId { get; set; } = Guid.Empty;
    public string StationName { get; set; } = string.Empty;
    public string LocalApiHost { get; set; } = string.Empty;
    public string LocalApiPort { get; set; } = "443";
    public string LocalApiUserName { get; set; } = string.Empty;
    public string LocalApiPassword { get; set; } = string.Empty;

    // Kalibrasyon Ayarları
    public double PhZeroRef { get; set; } = 7.0;
    public double PhSpanRef { get; set; } = 4.0;
    public int PhCalibrationDuration { get; set; } = 60; // saniye
    public double IletkenlikZeroRef { get; set; } = 0.0;
    public double IletkenlikSpanRef { get; set; } = 1413.0;
    public int IletkenlikCalibrationDuration { get; set; } = 60; // saniye

    // Mail Sunucu Ayarları
    public string SmtpHost { get; set; } = string.Empty;
    public int SmtpPort { get; set; } = 587;
    public string SmtpUserName { get; set; } = string.Empty;
    public string SmtpPassword { get; set; } = string.Empty;
    public bool SmtpUseSsl { get; set; } = true;

    /// <summary>
    /// Tüm zorunlu alanların dolu olup olmadığını kontrol eder
    /// </summary>
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(PlcIpAddress)
            && !string.IsNullOrWhiteSpace(SaisUserName)
            && !string.IsNullOrWhiteSpace(SaisPassword)
            && StationId != Guid.Empty
            && !string.IsNullOrWhiteSpace(StationName)
            && !string.IsNullOrWhiteSpace(SmtpHost);
    }
}
