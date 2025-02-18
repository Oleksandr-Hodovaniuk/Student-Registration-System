using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Services;
using Core.Entities;
using FluentAssertions;
using Moq;

namespace StudentRegistrationSystem.BackendTests.Unit.Services;

[TestFixture]
public class TopicServiceTests
{
    private Mock<ITopicRepository> mockRepository;
    private TopicService topicService;

    [SetUp]
    public void SetUp()
    {
        mockRepository = new Mock<ITopicRepository>();
        //topicService = new TopicService(mockRepository.Object);
    }

    [Test]
    public async Task CreateAsync_TopicCreateDTOValid_ShouldReturnTopicDTO()
    {
        // Arrange
        var createDto = new TopicCreateDTO { Name = "C++" };
        var mockTopic = new Topic { Id = Guid.NewGuid(), Name = createDto.Name };

        mockRepository.Setup(r => r.CreateAsync(It.IsAny<Topic>())).ReturnsAsync(mockTopic);

        // Act
        var result = await topicService.CreateAsync(createDto);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(mockTopic.Id);
        result.Name.Should().Be(createDto.Name);
    }
}
