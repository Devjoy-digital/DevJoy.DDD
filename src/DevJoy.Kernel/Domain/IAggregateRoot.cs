using DevJoy.Events;

namespace DevJoy.Domain;

/// <summary>
/// A marker interface denoting that an Entity is an AggregateRoot
/// </summary>
public interface IAggregateRoot<TId> : IEntity<TId>
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    public void ClearDomainEvents();


    public IReadOnlyCollection<IExternalEvent> ExternalEvents { get; }
    public void ClearExternalEvents();
}
