namespace ISKI.IBKS.Domain.Common.Entities;

public abstract class AuditableEntity<TId> : Entity<TId>
{
    public DateTime CreatedAtUtc { get; internal set; }
    public DateTime? UpdatedAtUtc { get; internal set; }
    public DateTime? DeletedAtUtc { get; internal set; }
}
