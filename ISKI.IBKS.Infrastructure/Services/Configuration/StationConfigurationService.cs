using ISKI.IBKS.Application.Common.Configuration;
using ISKI.IBKS.Application.Common.IoT.Plc;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ISKI.IBKS.Infrastructure.Services.Configuration;

public class StationConfigurationService : IStationConfiguration
{
    private readonly IConfiguration _configuration;

    public StationConfigurationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Guid StationId => Guid.TryParse(_configuration["Station:StationId"], out var id) ? id : Guid.Empty;
    public string StationName => _configuration["Station:StationName"] ?? "Unknown Station";
    public string? StationCode => _configuration["Station:Code"];
    public int DataPeriodMinute => _configuration.GetValue<int>("Station:DataPeriodMinute", 1);

    public LocalApiSettings LocalApi => new LocalApiSettings
    {
        Host = _configuration["Station:LocalApi:Host"] ?? "localhost",
        Port = _configuration.GetValue<int>("Station:LocalApi:Port", 441),
        UserName = _configuration["Station:LocalApi:UserName"] ?? "iski",
        Password = _configuration["Station:LocalApi:Password"] ?? "iski"
    };

    public SaisSettings Sais => new SaisSettings
    {
        BaseUrl = _configuration["SAIS:BaseUrl"] ?? string.Empty,
        LoginUrl = _configuration["SAIS:LoginUrl"] ?? "/Security/login",
        UserName = _configuration["SAIS:UserName"] ?? string.Empty,
        Password = _configuration["SAIS:Password"] ?? string.Empty,
        TicketHeaderName = _configuration["SAIS:TicketHeaderName"] ?? "AToken",
        Timeout = TimeSpan.TryParse(_configuration["SAIS:Timeout"], out var t) ? t : TimeSpan.FromSeconds(30)
    };

    public string PlcIp => _configuration["Plc:Station:IpAddress"] ?? "127.0.0.1";
    public int PlcRack => _configuration.GetValue<int>("Plc:Station:Rack", 0);
    public int PlcSlot => _configuration.GetValue<int>("Plc:Station:Slot", 1);

    public PlcSettings Plc => new PlcSettings
    {
        Station = new PlcStationConfig
        {
            IpAddress = PlcIp,
            Rack = PlcRack,
            Slot = PlcSlot
        }
    };

    public CalibrationSettings Calibration => new CalibrationSettings
    {
        PhZeroDuration = _configuration.GetValue<int>("Station:PhZeroDuration", 60),
        PhZeroReference = _configuration.GetValue<double>("Station:PhZeroReference", 7.0),
        PhSpanDuration = _configuration.GetValue<int>("Station:PhSpanDuration", 60),
        PhSpanReference = _configuration.GetValue<double>("Station:PhSpanReference", 4.0),

        ConductivityZeroDuration = _configuration.GetValue<int>("Station:ConductivityZeroDuration", 60),
        ConductivityZeroReference = _configuration.GetValue<double>("Station:ConductivityZeroReference", 0),
        ConductivitySpanDuration = _configuration.GetValue<int>("Station:ConductivitySpanDuration", 60),
        ConductivitySpanReference = _configuration.GetValue<double>("Station:ConductivitySpanReference", 1413),

        AkmZeroDuration = _configuration.GetValue<int>("Station:AkmZeroDuration", 60),
        AkmZeroReference = _configuration.GetValue<double>("Station:AkmZeroReference", 0),

        KoiZeroDuration = _configuration.GetValue<int>("Station:KoiZeroDuration", 60),
        KoiZeroReference = _configuration.GetValue<double>("Station:KoiZeroReference", 0)
    };

    public string? Company => _configuration["Station:Company"];
    public DateTime? SetupDate => DateTime.TryParse(_configuration["Station:SetupDate"], out var d) ? d : null;
    public string? Address => _configuration["Station:Address"];

    public IReadOnlyList<string> SelectedSensors =>
        _configuration.GetSection("Plc:SelectedSensors").Get<string[]>() ?? Array.Empty<string>();

    public MailSettings Mail => new MailSettings
    {
        SmtpHost = _configuration["MailSettings:SmtpHost"] ?? string.Empty,
        SmtpPort = _configuration.GetValue<int>("MailSettings:SmtpPort", 587),
        Username = _configuration["MailSettings:Username"] ?? string.Empty,
        Password = _configuration["MailSettings:Password"] ?? string.Empty,
        UseSsl = bool.TryParse(_configuration["MailSettings:UseSsl"], out var ssl) ? ssl : true,
        FromAddress = _configuration["MailSettings:FromAddress"] ?? string.Empty,
        FromName = _configuration["MailSettings:FromName"] ?? "IBKS Alarm Sistemi"
    };

    public async Task SaveStationIdAndNameAsync(Guid stationId, string name)
    {
        var config = await LoadConfigAsync("station.json");

        if (!config.ContainsKey("Station")) config["Station"] = new Dictionary<string, object>();
        var stationSection = ToDictionary(config["Station"]);

        stationSection["StationId"] = stationId;
        stationSection["StationName"] = name;
        config["Station"] = stationSection;

        await SaveConfigAsync("station.json", config);
    }

    public async Task SaveCalibrationSettingsAsync(CalibrationSettings settings)
    {
        var config = await LoadConfigAsync("station.json");

        if (!config.ContainsKey("Station")) config["Station"] = new Dictionary<string, object>();
        var stationSection = ToDictionary(config["Station"]);

        stationSection["PhZeroDuration"] = settings.PhZeroDuration;
        stationSection["PhZeroReference"] = settings.PhZeroReference;
        stationSection["PhSpanDuration"] = settings.PhSpanDuration;
        stationSection["PhSpanReference"] = settings.PhSpanReference;

        stationSection["ConductivityZeroDuration"] = settings.ConductivityZeroDuration;
        stationSection["ConductivityZeroReference"] = settings.ConductivityZeroReference;
        stationSection["ConductivitySpanDuration"] = settings.ConductivitySpanDuration;
        stationSection["ConductivitySpanReference"] = settings.ConductivitySpanReference;

        stationSection["AkmZeroDuration"] = settings.AkmZeroDuration;
        stationSection["AkmZeroReference"] = settings.AkmZeroReference;

        stationSection["KoiZeroDuration"] = settings.KoiZeroDuration;
        stationSection["KoiZeroReference"] = settings.KoiZeroReference;

        config["Station"] = stationSection;

        await SaveConfigAsync("station.json", config);
    }

    public async Task SaveSaisSettingsAsync(SaisSettings settings)
    {
        var config = await LoadConfigAsync("sais.json");
        if (!config.ContainsKey("SAIS")) config["SAIS"] = new Dictionary<string, object>();
        var saisSection = ToDictionary(config["SAIS"]);

        saisSection["BaseUrl"] = settings.BaseUrl;
        saisSection["LoginUrl"] = settings.LoginUrl;
        saisSection["UserName"] = settings.UserName;
        saisSection["Password"] = settings.Password;
        saisSection["TicketHeaderName"] = settings.TicketHeaderName;
        saisSection["Timeout"] = settings.Timeout.ToString(@"hh\:mm\:ss");

        config["SAIS"] = saisSection;
        await SaveConfigAsync("sais.json", config);
    }

    public async Task SavePlcSettingsAsync(string ip, int rack, int slot, List<string> selectedSensors)
    {
        var config = await LoadConfigAsync("plc.json");
        if (!config.ContainsKey("Plc")) config["Plc"] = new Dictionary<string, object>();
        var plcSection = ToDictionary(config["Plc"]);

        if (!plcSection.ContainsKey("Station")) plcSection["Station"] = new Dictionary<string, object>();
        var stationSection = ToDictionary(plcSection["Station"]);

        stationSection["IpAddress"] = ip;
        stationSection["Rack"] = rack;
        stationSection["Slot"] = slot;
        
        plcSection["Station"] = stationSection;
        plcSection["SelectedSensors"] = selectedSensors;
        config["Plc"] = plcSection;

        await SaveConfigAsync("plc.json", config);
    }

    public async Task SaveMailSettingsAsync(string host, int port, string user, string pass, bool useSsl)
    {
        var config = await LoadConfigAsync("mail.json");
        if (!config.ContainsKey("MailSettings")) config["MailSettings"] = new Dictionary<string, object>();
        var mailSection = ToDictionary(config["MailSettings"]);

        mailSection["SmtpHost"] = host;
        mailSection["SmtpPort"] = port.ToString();
        mailSection["Username"] = user;
        mailSection["Password"] = pass;
        mailSection["UseSsl"] = useSsl.ToString().ToLower();
        
        // Ensure defaults if missing
        if (!mailSection.ContainsKey("FromAddress")) mailSection["FromAddress"] = user;
        if (!mailSection.ContainsKey("FromName")) mailSection["FromName"] = "IBKS Alarm Sistemi";

        config["MailSettings"] = mailSection;
        await SaveConfigAsync("mail.json", config);
    }

    private async Task<Dictionary<string, object>> LoadConfigAsync(string fileName)
    {
        var filePath = GetConfigPath(fileName);
        if (!File.Exists(filePath)) return new Dictionary<string, object>();

        var json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<Dictionary<string, object>>(json) ?? new Dictionary<string, object>();
    }

    private async Task SaveConfigAsync(string fileName, Dictionary<string, object> config)
    {
        var filePath = GetConfigPath(fileName);
        var dir = Path.GetDirectoryName(filePath);
        if (dir != null && !Directory.Exists(dir)) Directory.CreateDirectory(dir);

        await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true }));
        
        if (_configuration is IConfigurationRoot root)
        {
            root.Reload();
        }
    }

    private string GetConfigPath(string fileName)
    {
        var configDir = Path.Combine(AppContext.BaseDirectory, "Configuration");
        // We prioritize the AppContext.BaseDirectory/Configuration folder for consistency
        return Path.Combine(configDir, fileName);
    }

    private Dictionary<string, object> ToDictionary(object obj)
    {
        if (obj is Dictionary<string, object> dict) return dict;
        if (obj is JsonElement elem) return JsonSerializer.Deserialize<Dictionary<string, object>>(elem.GetRawText()) ?? new Dictionary<string, object>();
        return JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(obj)) ?? new Dictionary<string, object>();
    }
}

