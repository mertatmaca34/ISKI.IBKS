using ISKI.IBKS.Domain.Common.Entities;

namespace ISKI.IBKS.Domain.Entities;

/// <summary>
/// Enerji kesintisi kayıtlarını temsil eder.
/// SAIS GetPowerOffTimes ve SendPowerOffTime servisleri için kullanılır.
/// </summary>
public sealed class PowerOffTime : AuditableEntity<Guid>
{
    public Guid StationId { get; private set; }
    
    /// <summary>
    /// Enerji kesintisi başlangıç zamanı
    /// </summary>
    public DateTime StartDate { get; private set; }
    
    /// <summary>
    /// Enerji geri gelme zamanı (null ise hala kesintide)
    /// </summary>
    public DateTime? EndDate { get; private set; }
    
    /// <summary>
    /// Kesinti süresi (dakika)
    /// </summary>
    public int? DurationMinutes => EndDate.HasValue 
        ? (int)(EndDate.Value - StartDate).TotalMinutes 
        : null;
    
    /// <summary>
    /// SAIS'e bildirildi mi?
    /// </summary>
    public bool IsSentToSais { get; private set; }
    public DateTime? SentToSaisAt { get; private set; }

    private PowerOffTime() { }

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public PowerOffTime(Guid stationId, DateTime startDate)
    {
        Id = Guid.NewGuid();
        StationId = stationId;
        StartDate = startDate;
        EndDate = null;
        IsSentToSais = false;
    }

    public void SetEndDate(DateTime endDate)
    {
        if (endDate < StartDate)
            throw new ArgumentException("Bitiş tarihi başlangıç tarihinden önce olamaz.", nameof(endDate));
        
        EndDate = endDate;
    }

    public void MarkAsSentToSais()
    {
        IsSentToSais = true;
        SentToSaisAt = DateTime.UtcNow;
    }

    public bool IsOngoing => !EndDate.HasValue;
}
