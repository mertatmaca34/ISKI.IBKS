using Microsoft.Extensions.Configuration;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Model;

public class SetupState
{
    public string PlcIpAddress { get; set; } = "10.33.x.253";
    public int PlcRack { get; set; } = 0;
    public int PlcSlot { get; set; } = 1;
    public List<string> PlcSelectedSensors { get; set; } = new();

    public string SaisApiUrl { get; set; } = "https://entegrationsais.csb.gov.tr/";
    public string SaisUserName { get; set; } = "saisUsr";
    public string SaisPassword { get; set; } = "saisPwd";

    public Guid StationId { get; set; } = Guid.Empty;
    public string StationName { get; set; } = string.Empty;
    public string LocalApiHost { get; set; } = string.Empty;
    public string LocalApiPort { get; set; } = "441";
    public string LocalApiUserName { get; set; } = string.Empty;
    public string LocalApiPassword { get; set; } = string.Empty;

    public double PhZeroRef { get; set; } = 7.0;
    public double PhSpanRef { get; set; } = 4.0;
    public int PhCalibrationDuration { get; set; } = 60;
    public double CondZeroRef { get; set; } = 0.0;
    public double CondSpanRef { get; set; } = 1413.0;
    public int CondCalibrationDuration { get; set; } = 60;

    public string SmtpHost { get; set; } = string.Empty;
    public int SmtpPort { get; set; } = 587;
    public string SmtpUser { get; set; } = string.Empty;
    public string SmtpPass { get; set; } = string.Empty;
    public bool UseSsl { get; set; } = true;

    public void UpdateFromConfiguration(IConfiguration config)
    {
        PlcIpAddress = config["Plc:Station:IpAddress"] ?? PlcIpAddress;
        PlcRack = int.TryParse(config["Plc:Station:Rack"], out var rack) ? rack : PlcRack;
        PlcSlot = int.TryParse(config["Plc:Station:Slot"], out var slot) ? slot : PlcSlot;

        SaisApiUrl = config["SAIS:BaseUrl2"] ?? SaisApiUrl;
        SaisUserName = config["SAIS:UserName"] ?? SaisUserName;
        SaisPassword = config["SAIS:Password"] ?? SaisPassword;

        if (Guid.TryParse(config["Station:StationId"], out var id)) StationId = id;
        StationName = config["Station:StationName"] ?? StationName;
        LocalApiHost = config["Station:LocalApi:Host"] ?? LocalApiHost;
        LocalApiPort = config["Station:LocalApi:Port"] ?? LocalApiPort;
        LocalApiUserName = config["Station:LocalApi:UserName"] ?? LocalApiUserName;
        LocalApiPassword = config["Station:LocalApi:Password"] ?? LocalApiPassword;

        PhZeroRef = double.TryParse(config["Calibration:Ph:ZeroRef"], out var pz) ? pz : PhZeroRef;
        PhSpanRef = double.TryParse(config["Calibration:Ph:SpanRef"], out var ps) ? ps : PhSpanRef;
        PhCalibrationDuration = int.TryParse(config["Calibration:Ph:Duration"], out var pd) ? pd : PhCalibrationDuration;
        CondZeroRef = double.TryParse(config["Calibration:Iletkenlik:ZeroRef"], out var iz) ? iz : CondZeroRef;
        CondSpanRef = double.TryParse(config["Calibration:Iletkenlik:SpanRef"], out var isr) ? isr : CondSpanRef;
        CondCalibrationDuration = int.TryParse(config["Calibration:Iletkenlik:Duration"], out var idur) ? idur : CondCalibrationDuration;

        SmtpHost = config["MailSettings:SmtpHost"] ?? SmtpHost;
        SmtpPort = int.TryParse(config["MailSettings:SmtpPort"], out var mp) ? mp : SmtpPort;
        SmtpUser = config["MailSettings:Username"] ?? SmtpUser;
        SmtpPass = config["MailSettings:Password"] ?? SmtpPass;
        UseSsl = bool.TryParse(config["MailSettings:UseSsl"], out var ssl) ? ssl : UseSsl;
    }

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
