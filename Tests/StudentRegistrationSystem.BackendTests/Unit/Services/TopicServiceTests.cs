using Application.DTOs;
using Application.Exceptions;
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
    public async Task CreateAsync_TopicDoesntExist_ShouldReturnTopicDTO()
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

    [Test]
    public async Task CreateAsync_TopicExists_ShouldThrowBusinessException()
    {
        // Arrange
        var dto = new TopicCreateDTO { Name = "C++" };

        mockRepository.Setup(r => r.ExistsByNameAsync(dto.Name)).ReturnsAsync(true);

        // Act
        var action = async () => await topicService.CreateAsync(dto);

        // Assert
        await action.Should().ThrowAsync<BusinessException>()
            .WithMessage($"Topic with name: {dto.Name} already exists.");
    }

    [Test]
    public async Task DeleteAsync_TopicExists_ShouldReturnTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        mockRepository.Setup(r => r.ExistsByIdAsync(id)).ReturnsAsync(true);
        mockRepository.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);

        // Act
        var result = await topicService.DeleteAsync(id);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public async Task DeleteAsync_TopicDoesntExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();
        mockRepository.Setup(r => r.ExistsByIdAsync(id)).ReturnsAsync(false);

        // Act
        var result = async () => await topicService.DeleteAsync(id);

        // Assert
        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Topic with id: {id} doesn't exist.");
    }

    [Test]
    public async Task GetAllAsync_TopicsExist_ShouldReturnTopics()
    {
        // Arrange
        var mockTopics = new List<Topic>
        {
            new Topic { Id = Guid.NewGuid(), Name = "C++"},
            new Topic { Id = Guid.NewGuid(), Name = "HTML"}
        };

        var mockTopicDTOs = new List<TopicDTO>
        {
            new TopicDTO { Id = mockTopics[0].Id, Name = mockTopics[0].Name},
            new TopicDTO { Id = mockTopics[1].Id, Name = mockTopics[1].Name}
        };

        mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(mockTopics);
        mockMapper.Setup(m => m.Map<IEnumerable<TopicDTO>>(It.IsAny<IEnumerable<Topic>>())).Returns(mockTopicDTOs);

        // Act
        var result = await topicService.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Equal(mockTopicDTOs);
    }

    [Test]
    public async Task GetAllAsync_TopicsDontExist_ShouldReturnEmpty()
    {
        // Arrange
        var mockTopics = new List<Topic>();

        var mockTopicDTOs = new List<TopicDTO>();

        mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(mockTopics);
        mockMapper.Setup(m => m.Map<IEnumerable<TopicDTO>>(It.IsAny<IEnumerable<Topic>>())).Returns(mockTopicDTOs);

        // Act
        var result = await topicService.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}
