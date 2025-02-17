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
    public async Task GetAllAsync_WhenTopicsDontExists_ShouldReturnEmpty()
    {
        // Act
        var result = await repository.GetAllAsync();

        //Assert
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

        //Assert
        result.Should().BeTrue();
        exists.Should().BeFalse();
    }

    [Test]
    public async Task DeleteAsync_WhenIdDontExists_ShouldReturnFalse()
    {
        // Act
        var result = await repository.DeleteAsync(Guid.NewGuid());

        //Assert
        result.Should().BeFalse();
    }
}
