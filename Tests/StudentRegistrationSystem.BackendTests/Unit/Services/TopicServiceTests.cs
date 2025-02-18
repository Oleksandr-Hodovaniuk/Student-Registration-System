using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Services;
using AutoMapper;
using Core.Entities;
using FluentAssertions;
using Moq;

namespace StudentRegistrationSystem.BackendTests.Unit.Services;

[TestFixture]
public class TopicServiceTests
{
    private Mock<ITopicRepository> mockRepository;
    private Mock<IMapper> mockMapper;
    private TopicService topicService;

    [SetUp]
    public void SetUp()
    {
        mockRepository = new Mock<ITopicRepository>();
        mockMapper = new Mock<IMapper>();
        topicService = new TopicService(mockRepository.Object, mockMapper.Object);
    }

    [Test]
    public async Task CreateAsync_TopicCreateDTOValid_ShouldReturnTopicDTO()
    {
        // Arrange
        var createDto = new TopicCreateDTO { Name = "C++" };
        var mockTopic = new Topic { Id = Guid.NewGuid(), Name = createDto.Name };
        var expectedDto = new TopicDTO { Id = mockTopic.Id, Name = mockTopic.Name };

        mockRepository.Setup(r => r.CreateAsync(It.IsAny<Topic>())).ReturnsAsync(mockTopic);
        mockMapper.Setup(mapper => mapper.Map<TopicDTO>(It.IsAny<Topic>())).Returns(expectedDto);

        // Act
        var result = await topicService.CreateAsync(createDto);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(mockTopic.Id);
        result.Name.Should().Be(createDto.Name);
    }
}
