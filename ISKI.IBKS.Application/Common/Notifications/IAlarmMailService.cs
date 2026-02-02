using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.Notifications;

public interface IAlarmMailService
{
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

    Task<int> SendAlarmToSubscribersAsync(
        Guid alarmDefinitionId,
        string alarmTitle,
        string alarmDescription,
        string sensorName,
        double? currentValue,
        double? thresholdValue,
        DateTime occurredAt,
        CancellationToken ct = default);

    Task<bool> SendTestEmailAsync(string toEmail, CancellationToken ct = default);
}
