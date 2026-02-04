namespace ISKI.IBKS.Application.Services.Mail;

/// <summary>
/// Alarm e-posta bildirim servisi arayüzü
/// </summary>
public interface IAlarmMailService
{
    /// <summary>
    /// Tek bir kullanıcıya alarm bildirimi gönderir
    /// </summary>
    Task<bool> SendAlarmNotificationAsync(
        string toEmail,
        string toName,
        string alarmTitle,
        string alarmDescription,
        string sensorName,
        double? currentValue,
        double? thresholdValue,
        DateTime occurredAt,
        CancellationToken ct = default);

    /// <summary>
    /// Alarm tanımına abone olan tüm kullanıcılara bildirim gönderir
    /// </summary>
    Task<int> SendAlarmToSubscribersAsync(
        Guid alarmDefinitionId,
        string alarmTitle,
        string alarmDescription,
        string sensorName,
        double? currentValue,
        double? thresholdValue,
        DateTime occurredAt,
        CancellationToken ct = default);

    /// <summary>
    /// Test e-postası gönderir
    /// </summary>
    Task<bool> SendTestEmailAsync(string toEmail, CancellationToken ct = default);
}
