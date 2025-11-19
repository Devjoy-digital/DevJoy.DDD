

using DevJoy.Events;

using System.Linq;

namespace DevJoy.Domain
{
    /// <summary>
    /// The base class for an aggregate root that provides the basic properties
    /// of an entity, and implements the marker interface <see cref="IAggregateRoot"/>
    /// </summary>
    public class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
    {

        private List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (_domainEvents.Contains(domainEvent))
            {
                throw new Exception("An instance of a domain every were added multiple times.");
            }
            _domainEvents.Add(domainEvent);
        }
        public void ClearDomainEvents() => _domainEvents.Clear();


        private List<IExternalEvent> _externalEvents = new();
        public IReadOnlyCollection<IExternalEvent> ExternalEvents => _externalEvents;
        public void AddExternalEvent(IExternalEvent externalEvent)
        {
            if (_externalEvents.Contains(externalEvent))
            {
                throw new Exception("An instance of an external every were added multiple times.");
            }
            _externalEvents.Add(externalEvent);
        }
        public void ClearExternalEvents() => _externalEvents.Clear();
    }

    /// <summary>
    /// The default base class for all aggregate roots that use a <see cref="Guid"/> as the identifier.
    /// </summary>
    public class AggregateRoot : AggregateRoot<Guid> { }
}
