using ISKI.IBKS.Domain.Common.Entities;

namespace ISKI.IBKS.Domain.Entities;

public sealed class PowerOffTime : AuditableEntity<Guid>
{
    public Guid StationId { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime? EndDate { get; private set; }

    public int? DurationMinutes => EndDate.HasValue
        ? (int)(EndDate.Value - StartDate).TotalMinutes
        : null;

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
            throw new ArgumentException("BitiÃ…Å¸ tarihi baÃ…Å¸langÃ„Â±ÃƒÂ§ tarihinden ÃƒÂ¶nce olamaz.", nameof(endDate));

        EndDate = endDate;
    }

    public void MarkAsSentToSais()
    {
        IsSentToSais = true;
        SentToSaisAt = DateTime.UtcNow;
    }

    public bool IsOngoing => !EndDate.HasValue;
}

