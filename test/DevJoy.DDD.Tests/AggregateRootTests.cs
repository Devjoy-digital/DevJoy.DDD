using DevJoy.Domain;
using DevJoy.Events;
using System;
using System.Collections.Generic;
using Xunit;

namespace DevJoy.DDD.Tests
{
    public class AggregateRootTests
    {
        private class TestAggregate : AggregateRoot
        {
            public void AddDomainEventPublic(IDomainEvent domainEvent, bool skipDuplicateCheck = false)
            {
                AddDomainEvent(domainEvent, skipDuplicateCheck);
            }

            public void AddExternalEventPublic(IExternalEvent externalEvent, bool skipDuplicateCheck = false)
            {
                AddExternalEvent(externalEvent, skipDuplicateCheck);
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
            aggregate.AddDomainEventPublic(domainEvent);

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
            aggregate.AddExternalEventPublic(externalEvent);

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
            aggregate.AddDomainEventPublic(domainEvent);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => aggregate.AddDomainEventPublic(domainEvent));
            Assert.Contains("domain every were added multiple times", exception.Message);
        }

        [Fact]
        public void AggregateRoot_ShouldThrow_WhenAddingSameExternalEventTwice()
        {
            // Arrange
            var aggregate = new TestAggregate();
            var externalEvent = new TestExternalEvent();
            aggregate.AddExternalEventPublic(externalEvent);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => aggregate.AddExternalEventPublic(externalEvent));
            Assert.Contains("external every were added multiple times", exception.Message);
        }

        [Fact]
        public void AggregateRoot_ShouldClear_DomainEvents()
        {
            // Arrange
            var aggregate = new TestAggregate();
            aggregate.AddDomainEventPublic(new TestDomainEvent());
            aggregate.AddDomainEventPublic(new TestDomainEvent(), true);

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
            aggregate.AddExternalEventPublic(new TestExternalEvent());
            aggregate.AddExternalEventPublic(new TestExternalEvent(), true);

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
}
