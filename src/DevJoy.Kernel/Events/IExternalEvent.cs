

namespace DevJoy.Events;

/// <summary>
/// An eventy type intended for messaging across bounded contexts.
/// </summary>
/// <remarks>Used to mark external events that are intended to be published to a message broker.</remarks>
public interface IExternalEvent { }

public interface IAuditableExternalEvent : IAuditable { }
