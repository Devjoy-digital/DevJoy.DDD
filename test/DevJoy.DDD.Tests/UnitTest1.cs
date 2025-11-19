using DevJoy.Domain;

namespace DevJoy.DDD.Tests;

public class EntityTests
{
    private class TestEntity : Entity
    {
        public string Name { get; set; } = string.Empty;
    }

    private class TestEntityWithCustomId : Entity<int>
    {
        public TestEntityWithCustomId(int id)
        {
            Id = id;
        }
    }

    [Fact]
    public void Entity_ShouldInitialize_WithDefaultGuidId()
    {
        // Arrange & Act
        var entity = new TestEntity();

        // Assert
        // Entity initializes with default value (Guid.Empty)
        // Derived classes should set their own Id values
        Assert.Equal(Guid.Empty, entity.Id);
    }

    [Fact]
    public void Entity_ShouldHave_CreatedAtTimestamp()
    {
        // Arrange
        var beforeCreation = DateTimeOffset.Now;

        // Act
        var entity = new TestEntity();

        // Assert
        var afterCreation = DateTimeOffset.Now;
        Assert.InRange(entity.CreatedAt, beforeCreation.AddSeconds(-1), afterCreation);
    }

    [Fact]
    public void Entity_ShouldHave_UpdatedAtTimestamp()
    {
        // Arrange
        var beforeCreation = DateTimeOffset.Now;

        // Act
        var entity = new TestEntity();

        // Assert
        var afterCreation = DateTimeOffset.Now;
        Assert.InRange(entity.UpdatedAt, beforeCreation.AddSeconds(-1), afterCreation);
    }

    [Fact]
    public void Entity_ShouldInitialize_WithVersionZero()
    {
        // Arrange & Act
        var entity = new TestEntity();

        // Assert
        Assert.Equal(0, entity.Version);
    }

    [Fact]
    public void Entity_ShouldSupport_CustomIdType()
    {
        // Arrange
        var customId = 123;

        // Act
        var entity = new TestEntityWithCustomId(customId);

        // Assert
        Assert.Equal(customId, entity.Id);
    }
}
