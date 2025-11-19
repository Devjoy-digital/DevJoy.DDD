using DevJoy.Domain;
using DevJoy.Events;

namespace DevJoy.DDD.Tests;

public class AggregateRootTests
{
    private class TestAggregate : AggregateRoot
    {
        public void DoSomething()
        {
            AddDomainEvent(new TestDomainEvent());
        }

        public void DoSomethingExternal()
        {
            AddExternalEvent(new TestExternalEvent());
        }
    }

    private class TestDomainEvent : IDomainEvent { }

    private class TestExternalEvent : IExternalEvent { }

    [Fact]
    public void AggregateRoot_ShouldInitialize_WithEmptyDomainEvents()
    {
        // Arrange & Act
        var aggregate = new TestAggregate();

        // Assert
        Assert.Empty(aggregate.DomainEvents);
    }

    [Fact]
    public void AggregateRoot_ShouldInitialize_WithEmptyExternalEvents()
    {
        // Arrange & Act
        var aggregate = new TestAggregate();

        // Assert
        Assert.Empty(aggregate.ExternalEvents);
    }

    [Fact]
    public void AggregateRoot_ShouldAdd_DomainEvent()
    {
        // Arrange
        var aggregate = new TestAggregate();
        var domainEvent = new TestDomainEvent();

        // Act
        aggregate.AddDomainEvent(domainEvent);

        // Assert
        Assert.Single(aggregate.DomainEvents);
        Assert.Contains(domainEvent, aggregate.DomainEvents);
    }

    [Fact]
    public void AggregateRoot_ShouldAdd_ExternalEvent()
    {
        // Arrange
        var aggregate = new TestAggregate();
        var externalEvent = new TestExternalEvent();

        // Act
        aggregate.AddExternalEvent(externalEvent);

        // Assert
        Assert.Single(aggregate.ExternalEvents);
        Assert.Contains(externalEvent, aggregate.ExternalEvents);
    }

    [Fact]
    public void AggregateRoot_ShouldThrow_WhenAddingSameDomainEventTwice()
    {
        // Arrange
        var aggregate = new TestAggregate();
        var domainEvent = new TestDomainEvent();
        aggregate.AddDomainEvent(domainEvent);

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => aggregate.AddDomainEvent(domainEvent));
        Assert.Contains("domain every were added multiple times", exception.Message);
    }

    [Fact]
    public void AggregateRoot_ShouldThrow_WhenAddingSameExternalEventTwice()
    {
        // Arrange
        var aggregate = new TestAggregate();
        var externalEvent = new TestExternalEvent();
        aggregate.AddExternalEvent(externalEvent);

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => aggregate.AddExternalEvent(externalEvent));
        Assert.Contains("external every were added multiple times", exception.Message);
    }

    [Fact]
    public void AggregateRoot_ShouldClear_DomainEvents()
    {
        // Arrange
        var aggregate = new TestAggregate();
        aggregate.AddDomainEvent(new TestDomainEvent());
        aggregate.AddDomainEvent(new TestDomainEvent());

        // Act
        aggregate.ClearDomainEvents();

        // Assert
        Assert.Empty(aggregate.DomainEvents);
    }

    [Fact]
    public void AggregateRoot_ShouldClear_ExternalEvents()
    {
        // Arrange
        var aggregate = new TestAggregate();
        aggregate.AddExternalEvent(new TestExternalEvent());
        aggregate.AddExternalEvent(new TestExternalEvent());

        // Act
        aggregate.ClearExternalEvents();

        // Assert
        Assert.Empty(aggregate.ExternalEvents);
    }

    [Fact]
    public void AggregateRoot_DomainEvents_ShouldBeReadOnly()
    {
        // Arrange
        var aggregate = new TestAggregate();

        // Act & Assert
        Assert.IsAssignableFrom<IReadOnlyCollection<IDomainEvent>>(aggregate.DomainEvents);
    }

    [Fact]
    public void AggregateRoot_ExternalEvents_ShouldBeReadOnly()
    {
        // Arrange
        var aggregate = new TestAggregate();

        // Act & Assert
        Assert.IsAssignableFrom<IReadOnlyCollection<IExternalEvent>>(aggregate.ExternalEvents);
    }

    [Fact]
    public void AggregateRoot_ShouldInherit_FromEntity()
    {
        // Arrange & Act
        var aggregate = new TestAggregate();

        // Assert
        Assert.IsAssignableFrom<Entity<Guid>>(aggregate);
        Assert.Equal(Guid.Empty, aggregate.Id);
        Assert.Equal(0, aggregate.Version);
    }
}
