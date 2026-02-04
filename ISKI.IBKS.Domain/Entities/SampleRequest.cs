using ISKI.IBKS.Domain.Common.Entities;

namespace ISKI.IBKS.Domain.Entities;

/// <summary>
/// Numune alma taleplerini temsil eder.
/// SAIS numune servisleri için kullanılır.
/// </summary>
public sealed class SampleRequest : AuditableEntity<Guid>
{
    public Guid StationId { get; private set; }
    
    /// <summary>
    /// SAIS tarafından verilen numune kodu
    /// </summary>
    public string SampleCode { get; private set; } = string.Empty;
    
    /// <summary>
    /// Numune alımına neden olan parametre (sınır aşımı durumunda)
    /// </summary>
    public string? TriggerParameter { get; private set; }
    
    /// <summary>
    /// Numune alma durumu
    /// </summary>
    public SampleStatus Status { get; private set; }
    
    /// <summary>
    /// Numune başlangıç zamanı
    /// </summary>
    public DateTime StartedAt { get; private set; }
    
    /// <summary>
    /// Numune bitiş zamanı
    /// </summary>
    public DateTime? CompletedAt { get; private set; }
    
    /// <summary>
    /// Tetikleme türü
    /// </summary>
    public SampleTriggerType TriggerType { get; private set; }
    
    /// <summary>
    /// Hata mesajı (başarısız durumda)
    /// </summary>
    public string? ErrorMessage { get; private set; }

    private SampleRequest() { }

    public static SampleRequest CreateFromSaisTrigger(Guid stationId, string sampleCode)
    {
        return new SampleRequest
        {
            Id = Guid.NewGuid(),
            StationId = stationId,
            SampleCode = sampleCode,
            TriggerType = SampleTriggerType.SaisRemote,
            Status = SampleStatus.Pending,
            StartedAt = DateTime.UtcNow
        };
    }

    public static SampleRequest CreateFromLimitOver(Guid stationId, string triggerParameter)
    {
        return new SampleRequest
        {
            Id = Guid.NewGuid(),
            StationId = stationId,
            TriggerParameter = triggerParameter,
            TriggerType = SampleTriggerType.LimitOver,
            Status = SampleStatus.Pending,
            StartedAt = DateTime.UtcNow
        };
    }

    public void SetSampleCode(string sampleCode)
    {
        SampleCode = sampleCode;
        Status = SampleStatus.Started;
    }

    public void MarkAsCompleted()
    {
        Status = SampleStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed(string errorMessage)
    {
        Status = SampleStatus.Failed;
        CompletedAt = DateTime.UtcNow;
        ErrorMessage = errorMessage;
    }
}

public enum SampleStatus
{
    Pending = 0,    // Bekliyor
    Started = 1,    // Başladı
    Completed = 2,  // Tamamlandı
    Failed = 3      // Başarısız
}

public enum SampleTriggerType
{
    SaisRemote = 0,  // SAIS tarafından tetiklendi
    LimitOver = 1,   // Sınır aşımı nedeniyle
    Manual = 2       // Manuel tetikleme
}
