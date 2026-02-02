using ISKI.IBKS.Application.Common.IoT.Plc;

namespace ISKI.IBKS.Application.Common.Configuration;

public interface IStationConfiguration
{
    Guid StationId { get; }
    string StationName { get; }
    string? StationCode { get; }
    int DataPeriodMinute { get; }

    SaisSettings Sais { get; }
    LocalApiSettings LocalApi { get; }
    PlcSettings Plc { get; }
    string PlcIp { get; }
    int PlcRack { get; }
    int PlcSlot { get; }
    CalibrationSettings Calibration { get; }

    IReadOnlyList<string> SelectedSensors { get; }

    string? Company { get; }
    DateTime? SetupDate { get; }
    string? Address { get; }
    MailSettings Mail { get; }

    Task SaveStationIdAndNameAsync(Guid stationId, string name);
    Task SaveCalibrationSettingsAsync(CalibrationSettings settings);
    Task SaveSaisSettingsAsync(SaisSettings settings);
    Task SavePlcSettingsAsync(string ip, int rack, int slot, List<string> selectedSensors);
    Task SaveMailSettingsAsync(string host, int port, string user, string pass, bool useSsl);
}

