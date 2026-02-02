namespace ISKI.IBKS.Domain.Common.Entities;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; } = default!;

    public List<IDomainEvent> DomainEvents { get; } = new();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        DomainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        DomainEvents.Clear();
    }
}

