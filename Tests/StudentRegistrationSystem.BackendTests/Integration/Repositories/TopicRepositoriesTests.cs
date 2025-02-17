using Core.Entities;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace StudentRegistrationSystem.BackendTests.Integration.Repositories;

[TestFixture]
public class TopicRepositoriesTests
{
    private StudentRegistrationSystemDbContext dbContext;
    private TopicRepository repository;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<StudentRegistrationSystemDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        dbContext = new StudentRegistrationSystemDbContext(options);
        repository = new TopicRepository(dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        dbContext.Dispose();
    }

    [Test]
    public async Task CreateAsync_WhenValidTopic_ShouldCreateAndReturnTopic()
    {
        // Arrange
        var topic = new Topic { Id = Guid.NewGuid(), Name = "C++" };

        // Act
        var result = await repository.CreateAsync(topic);
        var exists = await repository.ExistsByIdAsync(topic.Id);

        // Assert
        result.Should().NotBeNull();
        exists.Should().BeTrue();
    }

    [Test]
    public async Task GetAllAsync_WhenTopicsExist_ShouldReturnAllTopics()
    {
        // Arrange
        var topic1 = new Topic { Id = Guid.NewGuid(), Name = "C++" };
        var topic2 = new Topic { Id = Guid.NewGuid(), Name = "HTML" };

        await dbContext.AddRangeAsync(topic1, topic2);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
    }

    [Test]
    public async Task GetAllAsync_WhenTopicsDoesntExist_ShouldReturnEmpty()
    {
        // Act
        var result = await repository.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task DeleteAsync_WhenIdExists_ShouldDeleteTopicAndReturnTrue()
    {
        // Arrange
        var topic = new Topic {Id = Guid.NewGuid(), Name = "C++" };
        
        await dbContext.AddAsync(topic);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.DeleteAsync(topic.Id);
        var exists = await repository.ExistsByIdAsync(topic.Id);

        // Assert
        result.Should().BeTrue();
        exists.Should().BeFalse();
    }

    [Test]
    public async Task DeleteAsync_WhenIdDoesntExist_ShouldReturnFalse()
    {
        // Arrange
        var topicId = Guid.NewGuid();

        // Act
        var result = await repository.DeleteAsync(topicId);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public async Task UpdateAsync_WhenValidTopic_ShouldUpdateAndReturnTopic()
    {
        // Arrange
        var topic = new Topic { Id = Guid.NewGuid(), Name = "C++" };

        await dbContext.AddAsync(topic);
        await dbContext.SaveChangesAsync();

        topic.Name = "HTML";

        // Act
        var result = await repository.UpdateAsync(topic);
        var updatedTopic = await repository.GetByIdAsync(topic.Id);
            
        // Assert
        updatedTopic.Should().NotBeNull();
        updatedTopic.Name.Should().Be(topic.Name);
    }

    [Test]
    public async Task ExistsByIdAsync_WhenTopicExists_ShouldReturnTrue()
    {
        // Arrange
        var topicId = Guid.NewGuid();
        var topic = new Topic { Id = topicId, Name = "C++" };

        await dbContext.AddAsync(topic);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.ExistsByIdAsync(topicId);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public async Task ExistsByIdAsync_WhenTopicDoesntExist_ShouldReturnFalse()
    {
        // Arrange
        var topicId = Guid.NewGuid();

        // Act
        var result = await repository.ExistsByIdAsync(topicId);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public async Task ExistsByStringAsync_WhenStringExists_ShouldReturnTrue()
    {
        // Arrange
        var topicName = "C++";
        var topic = new Topic { Id = Guid.NewGuid(), Name = topicName };

        await dbContext.AddAsync(topic);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.ExistsByStringAsync(topicName);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public async Task ExistsByStringAsync_WhenStringDoesntExist_ShouldReturnFalse()
    {
        // Arrange
        var topicName = "C++";

        // Act
        var result = await repository.ExistsByStringAsync(topicName);

        // Assert
        result.Should().BeFalse();
    }
}
