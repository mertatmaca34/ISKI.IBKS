using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ISKI.IBKS.Application.Features.Alarms;
using ISKI.IBKS.Application.Services.DataCollection; // Correct namespace
using ISKI.IBKS.Application.Services.Mail;
using ISKI.IBKS.Domain.Entities;
// using ISKI.IBKS.Infrastructure.IoT.Plc; // Removed
using ISKI.IBKS.Infrastructure.Services.Mail;
using ISKI.IBKS.Persistence.Contexts; // Correct namespace

namespace ISKI.IBKS.Infrastructure.Services.Alarms;

public class AlarmManager : IAlarmManager
{
    private readonly ILogger<AlarmManager> _logger;
    private readonly IAlarmMailService _mailService;
    private readonly IbksDbContext _dbContext;
    
    // In-memory state for debounce/anti-spam (DefinitionId -> State)
    // Key: AlarmDefinition.Id
    private static readonly ConcurrentDictionary<Guid, AlarmState> _activeAlarms = new();
    
    // Cache for reflection properties to avoid overhead
    private static readonly ConcurrentDictionary<string, PropertyInfo?> _propertyCache = new();

    // Anti-spam cooldown (e.g., 30 minutes)
    private readonly TimeSpan _notificationCooldown = TimeSpan.FromMinutes(30);

    public AlarmManager(
        ILogger<AlarmManager> logger,
        IAlarmMailService mailService,
        IbksDbContext dbContext)
    {
        _logger = logger;
        _mailService = mailService;
        _dbContext = dbContext;
    }

    public async Task ProcessAlarmsAsync(PlcDataSnapshot snapshot, CancellationToken ct = default)
    {
        try
        {
            // 1. Load active alarm definitions
            var definitions = await _dbContext.AlarmDefinitions
                .AsNoTracking()
                .Where(x => x.IsActive)
                .ToListAsync(ct);

            // 2. Load station settings (needed for station ID)
            var station = await _dbContext.StationSettings
                .AsNoTracking()
                .FirstOrDefaultAsync(ct);

            if (station == null)
            {
                _logger.LogWarning("Alarm kontrolü için istasyon ayarları bulunamadı.");
                return;
            }

            foreach (var def in definitions)
            {
                await EvaluateAlarmAsync(def, snapshot, station.StationId, ct);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Alarm işleme sırasında genel hata oluştu.");
        }
    }

    private async Task EvaluateAlarmAsync(AlarmDefinition def, PlcDataSnapshot snapshot, Guid stationId, CancellationToken ct)
    {
        try
        {
            // Reflection: Get property value from Snapshot
            var value = GetValueFromSnapshot(snapshot, def.SensorName);
            
            // Debug logging to trace alarm evaluation
            _logger.LogDebug("Alarm değerlendirme: {AlarmName} | Sensor: {Sensor} | Değer: {Value} | Tip: {Type} | Expected: {Expected}",
                def.Name, def.SensorName, value, def.Type, def.ExpectedDigitalValue);
            
            if (value == null)
            {
                _logger.LogWarning("Alarm sensör değeri bulunamadı: {AlarmName} | Sensor: {Sensor}. PlcDataSnapshot üzerinde bu property var mı?",
                    def.Name, def.SensorName);
                return;
            }
            
            bool isAlarmActive = false;
            string alarmMessage = string.Empty;
            double? triggerValue = null;

            // Check conditions
            if (def.Type == AlarmType.Threshold)
            {
                if (value is double val)
                {
                    triggerValue = val;
                    if (def.MaxThreshold.HasValue && val > def.MaxThreshold.Value)
                    {
                        isAlarmActive = true;
                        alarmMessage = $"{def.Description} (Değer: {val:F2} > Eşik: {def.MaxThreshold})";
                    }
                    else if (def.MinThreshold.HasValue && val < def.MinThreshold.Value)
                    {
                        isAlarmActive = true;
                        alarmMessage = $"{def.Description} (Değer: {val:F2} < Eşik: {def.MinThreshold})";
                    }
                }
            }
            else // Digital or System
            {
                // ExpectedDigitalValue represents the NORMAL/expected state.
                // Alarm triggers when current value DIFFERS from expected (i.e., abnormal condition).
                // Example: KabinDuman - ExpectedDigitalValue=false (no smoke is normal)
                //          If KabinDuman=true -> Alarm (smoke detected!)
                
                if (value is bool boolVal)
                {
                    triggerValue = boolVal ? 1.0 : 0.0;
                    // Alarm triggers when current value != expected (abnormal state)
                    if (def.ExpectedDigitalValue.HasValue && boolVal != def.ExpectedDigitalValue.Value)
                    {
                        isAlarmActive = true;
                        alarmMessage = $"{def.Description} (Durum: {boolVal})";
                    }
                }
                else if (value is double dVal) 
                {
                     // Convert double to bool if needed (0 = false, >0 = true)
                     bool convertedBool = dVal > 0;
                     // Alarm triggers when current value != expected (abnormal state)
                     if (def.ExpectedDigitalValue.HasValue && convertedBool != def.ExpectedDigitalValue.Value)
                     {
                        isAlarmActive = true;
                         alarmMessage = $"{def.Description} (Durum: {convertedBool})";
                     }
                }
            }

            if (isAlarmActive)
            {
                await HandleActiveAlarmAsync(def, stationId, alarmMessage, triggerValue, ct);
            }
            else
            {
                // Clear state if alarm normalized
                 _activeAlarms.TryRemove(def.Id, out _);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Alarm değerlendirme hatası: {AlarmName}", def.Name);
        }
    }

    private async Task HandleActiveAlarmAsync(AlarmDefinition def, Guid stationId, string message, double? triggerValue, CancellationToken ct)
    {
        var now = DateTime.UtcNow;
        bool shouldSendNotification = false;

        // 1. Update/Check State
        var state = _activeAlarms.GetOrAdd(def.Id, _ => new AlarmState 
        { 
            FirstDetectedAt = now, 
            LastNotificationSentAt = DateTime.MinValue 
        });

        // 2. Anti-Spam Check
        if (now - state.LastNotificationSentAt > _notificationCooldown)
        {
            shouldSendNotification = true;
            state.LastNotificationSentAt = now; // Update timestamp immediately to prevent race
        }

        // 3. Log to History (Always)
        
        var alarmEvent = new AlarmEvent(
            stationId,
            def.Id,
            def.SensorName, // TriggerKey
            message ?? "Alarm tetiklendi", // Message
            triggerValue, // TriggerValue
            def.Priority
        )
        {
            Id = Guid.NewGuid()
            // OccurredAt is read-only
        };

        if (shouldSendNotification)
        {
            try 
            {
                // 4. Get Subscribers
                // Filter: Active subscription, Active user, Receive Email enabled, Priority >= User's Minimum
                var subscribers = await _dbContext.AlarmUserSubscriptions
                    .Include(s => s.AlarmUser)
                    .AsNoTracking()
                    .Where(s => s.AlarmDefinitionId == def.Id 
                        && s.IsActive 
                        && s.AlarmUser.IsActive 
                        && s.AlarmUser.ReceiveEmailNotifications
                        && def.Priority >= s.AlarmUser.MinimumPriorityLevel) 
                    .Select(s => s.AlarmUser)
                    .ToListAsync(ct);

                if (subscribers.Any())
                {
                    int sentCount = 0;
                    foreach (var user in subscribers)
                    {
                        if (string.IsNullOrEmpty(user.Email)) continue;

                        bool sent = await _mailService.SendAlarmNotificationAsync(
                            user.Email,
                            user.FullName,
                            def.Name,
                            message,
                            def.SensorName,
                            triggerValue,
                            def.Type == AlarmType.Threshold ? def.MaxThreshold ?? def.MinThreshold : null,
                            now,
                            ct
                        );
                        
                        if (sent) sentCount++;
                    }

                    if (sentCount > 0)
                    {
                         alarmEvent.MarkNotificationSent();
                        _logger.LogInformation("Alarm bildirimi gönderildi: {AlarmName} - {Count} kullanıcı", def.Name, sentCount);
                    }
                    else
                    {
                        _logger.LogWarning("Alarm bildirimi gönderilemedi: {AlarmName}", def.Name);
                    }
                }
                else
                {
                     _logger.LogInformation("Alarm aktif ancak abone bulunamadı: {AlarmName}", def.Name);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alarm bildirimi gönderilirken hata: {AlarmName}", def.Name);
            }
        }
        else 
        {
             _logger.LogDebug("Alarm aktif fakat spam filtresine takıldı (veya cooldown): {AlarmName}", def.Name);
        }

        _dbContext.AlarmEvents.Add(alarmEvent);
        await _dbContext.SaveChangesAsync(ct);
    }

    private object? GetValueFromSnapshot(PlcDataSnapshot snapshot, string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName)) return null;

        var prop = _propertyCache.GetOrAdd(propertyName, name => 
            typeof(PlcDataSnapshot).GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase));

        return prop?.GetValue(snapshot);
    }

    private class AlarmState
    {
        public DateTime FirstDetectedAt { get; set; }
        public DateTime LastNotificationSentAt { get; set; }
    }
}
