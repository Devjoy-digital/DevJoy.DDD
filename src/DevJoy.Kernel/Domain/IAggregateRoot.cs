using DevJoy.Events;

namespace DevJoy.Domain;

/// <summary>
/// A marker interface denoting that an Entity is an AggregateRoot
/// </summary>
public interface IAggregateRoot<TId> : IEntity<TId>
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    public void AddDomainEvent(IDomainEvent domainEvent);

    public void ClearDomainEvents();


    public IReadOnlyCollection<IExternalEvent> ExternalEvents { get; }

    public void AddExternalEvent(IExternalEvent externalEvent);

    public void ClearExternalEvents();
}
