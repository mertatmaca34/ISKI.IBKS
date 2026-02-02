namespace ISKI.IBKS.Shared.Constants;

public static class ConfigurationConstants
{
    public const string ConfigurationDirectory = "Configuration";
    public const string ResourcesDirectory = "Resources";

    public static class Files
    {
        public const string AppSettings = "appsettings.json";
        public const string SaisConfig = "sais.json";
        public const string PlcConfig = "plc.json";
        public const string StationConfig = "station.json";
        public const string MailConfig = "mail.json";
        public const string CalibrationConfig = "calibration.json";
        public const string UiMappingConfig = "ui-mapping.json";
        public const string AppIcon = "icons8_water.ico";
    }

    public static class Sections
    {
        public const string Plc = "Plc";
        public const string Sais = "SAIS";
        public const string UiMapping = "UiMapping";
        public const string MailSettings = "MailSettings";
    }
}
