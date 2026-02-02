using System;

namespace ISKI.IBKS.Application.Common.IoT.Snapshots;

public record ChannelReadingDto(
    string ChannelName,
    double Value,
    string Unit,
    double MinValue,
    double MaxValue,
    bool IsAlarm);

public record DigitalReadingDto(
    string Name,
    bool Value,
    bool IsAlarm);

public record HealthSummaryDto(
    bool IsSystemHealthy,
    int WarningCount,
    int ErrorCount,
    string StatusMessage);

public record StationStatusDto(
    bool IsConnected,
    long UptimeTicks,
    TimeSpan DailyWashRemaining,
    TimeSpan WeeklyWashRemaining,
    DateTime SystemTime);
