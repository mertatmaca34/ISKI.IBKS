namespace ISKI.IBKS.Domain.Common.Entities;

public abstract class AuditableEntity<TId> : Entity<TId>
{
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
}

