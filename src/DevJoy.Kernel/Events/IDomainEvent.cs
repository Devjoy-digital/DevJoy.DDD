

namespace DevJoy.Events
{
    /// <summary>
    /// An event type intented for use within a bounded context.
    /// </summary>
    /// <remarks>Used to mark command and query evcents as well as any other domain events.</remarks>
    public interface IDomainEvent { }

    public interface IAuditableDomainEvent : IAuditable { }
}
