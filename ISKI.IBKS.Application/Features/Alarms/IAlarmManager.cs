using ISKI.IBKS.Application.Services.DataCollection;

namespace ISKI.IBKS.Application.Features.Alarms;

public interface IAlarmManager
{
    /// <summary>
    /// Processes sensor data against defined alarm rules.
    /// Manages state, logging, and notifications.
    /// </summary>
    Task ProcessAlarmsAsync(PlcDataSnapshot snapshot, CancellationToken ct = default);
}
