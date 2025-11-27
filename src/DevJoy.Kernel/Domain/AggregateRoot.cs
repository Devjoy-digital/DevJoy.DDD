

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
        // private fields
        private List<IDomainEvent> _domainEvents = new();
        private List<IExternalEvent> _externalEvents = new();


        // internal properties and methods
        protected void AddDomainEvent(IDomainEvent domainEvent, bool skipDuplicateCheck = false)
        {
            if (!skipDuplicateCheck && _domainEvents.Contains(domainEvent))
            {
                throw new Exception("An instance of a domain every were added multiple times.");
            }
            _domainEvents.Add(domainEvent);
        }
        protected void AddExternalEvent(IExternalEvent externalEvent, bool skipDuplicateCheck = false)
        {
            if (!skipDuplicateCheck && _externalEvents.Contains(externalEvent))
            {
                throw new Exception("An instance of an external every were added multiple times.");
            }
            _externalEvents.Add(externalEvent);
        }


        // public properties and methods
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;
        public void ClearDomainEvents() => _domainEvents.Clear();
        public IReadOnlyCollection<IExternalEvent> ExternalEvents => _externalEvents;
        public void ClearExternalEvents() => _externalEvents.Clear();
    }

    /// <summary>
    /// The default base class for all aggregate roots that use a <see cref="Guid"/> as the identifier.
    /// </summary>
    public class AggregateRoot : AggregateRoot<Guid> { }
}
