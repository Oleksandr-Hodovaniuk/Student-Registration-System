using Application.DTOs;
using Application.Mappers;
using AutoMapper;
using Core.Entities;
using FluentAssertions;
using System.Globalization;

namespace StudentRegistrationSystem.BackendTests.Unit.Mappers;

[TestFixture]
public class TopicProfileTests
{
    [Test]
    public void AutoMapper_ConfigurationIsValid()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<TopicProfile>());
        var mapper = config.CreateMapper();

        // Act & Assert
        config.AssertConfigurationIsValid();
    }

    [Test]
    public void Topic_To_TopicDTO_ShouldMapCorrectly()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<TopicProfile>());
        var mapper = config.CreateMapper();

        var topic = new Topic { Id = Guid.NewGuid(), Name = "C++" };

        // Act
        var dto = mapper.Map<TopicDTO>(topic);

        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(topic.Id);
        dto.Name.Should().Be(topic.Name);
    }

    [Test]
    public void TopicDTO_To_Topic_ShouldMapCorrectly()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<TopicProfile>());
        var mapper = config.CreateMapper();

        var dto = new Topic { Id = Guid.NewGuid(), Name = "C++" };

        // Act
        var topic = mapper.Map<Topic>(dto);

        // Assert
        topic.Should().NotBeNull();
        topic.Id.Should().Be(dto.Id);
        topic.Name.Should().Be(dto.Name);
    }

    [Test]
    public void TopicCreateDTO_To_Topic_ShouldMapCorrectly()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<TopicProfile>());
        var mapper = config.CreateMapper();

        var dto = new Topic { Name = "C++" };

        // Act
        var topic = mapper.Map<Topic>(dto);

        // Assert
        topic.Should().NotBeNull();
        topic.Id.Should().BeEmpty();
        topic.Name.Should().Be(dto.Name);
    }
}
