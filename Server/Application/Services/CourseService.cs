using Application.DTOs;
using Application.Exceptions;
using Application.Repositories;
using Application.Services.Interfaces;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CourseService(ICourseRepository repository, IMapper mapper, ILogger<CourseService> logger) : ICourseService
{
    public async Task<IEnumerable<CourseDTO>> GetAllAsync()
    {
        var courses = await repository.GetAllAsync();

        if (!courses.Any())
        {
            logger.LogWarning("Courses don't exist.");

            return Enumerable.Empty<CourseDTO>();
        }

        logger.LogInformation("Courses successfully returned.");

        return mapper.Map<IEnumerable<CourseDTO>>(courses);
    }

    public async Task<IEnumerable<CourseDTO>> GetAllByTopicsAsync( params int[] topicsIds)
    {
        var courses = await repository.GetAllByIdAsync(topicsIds);

        if (!courses.Any())
        {
            logger.LogWarning("Courses with these topics don't exist.");

            return Enumerable.Empty<CourseDTO>();
        }

        logger.LogInformation("Courses successfully returned.");

        return mapper.Map<IEnumerable<CourseDTO>>(courses);
    }

    public async Task<CourseDTO> GetByIdAsync(int id)
    {
        if (!await repository.ExistsByIdAsync(id))
        {
            logger.LogError($"Course with id: '{id}' doesn't exist.");

            throw new NotFoundException($"Course with id '{id}' doesn't exist.");
        }

        var course = await repository.GetByIdAsync(id);

        logger.LogInformation($"Course with id: '{id}' successfully returned.");

        return mapper.Map<CourseDTO>(course);
    }

    public async Task CreateAsync([FromBody] CreateCourseDTO dto)
    {
        if (await repository.ExistsByNameAsync(dto.Name))
        {
            logger.LogWarning($"Course with name: '{dto.Name}' already exists.");

            throw new BusinessException($"Course with name '{dto.Name}' already exists.");
        }

        var topics = await repository.TopicsExistsAsync(dto.Topics!);

        if (!topics.Any())
        {
            logger.LogError($"These topics don't exist.");

            throw new NotFoundException($"These topics don't exist.");
        }
        
        var course = mapper.Map<Course>(dto);
        course.Topics = topics;

        await repository.CreateAsync(course);

        logger.LogInformation($"Course: '{course.Name}' successfully created.");
    }

    public async Task UpdateAsync([FromBody] UpdateCourseDTO dto)
    {
        if (!await repository.ExistsByIdAsync(dto.Id))
        {
            logger.LogError($"Course with id: '{dto.Id}' doesn't exist.");

            throw new NotFoundException($"Course with id '{dto.Id}' doesn't exist.");     
        }

        var course = mapper.Map<Course>(dto);
        await repository.UpdateAsync(course);

        logger.LogInformation($"Course with id: '{course.Id}' successfully updated.");
    }

    public async Task DeleteAsync(int id)
    {
        if (!await repository.ExistsByIdAsync(id))
        {
            logger.LogError($"Course with id: '{id}' doesn't exist.");

            throw new NotFoundException($"Course with id '{id}' doesn't exist.");
        }

        await repository.DeleteAsync(id);

        logger.LogInformation($"Course with id: '{id}' successfully deleted.");
    }

    public async Task AddTopicAsync(int courseId, int topicId)
    {
        if (!await repository.ExistsByIdAsync(courseId))
        {
            logger.LogError($"Course with id: '{courseId}' doesn't exist.");

            throw new NotFoundException($"Course with id '{courseId}' doesn't exist.");
        }

        if (!await repository.TopicExistsByIdAsync(topicId))
        {
            logger.LogError($"Topic with id: '{topicId}' doesn't exist.");

            throw new NotFoundException($"Topic with id '{topicId}' doesn't exist.");
        }

        var course = await repository.GetByIdAsync(courseId);

        if (course!.Topics.Any(t => t.Id == topicId))
        {
            logger.LogError($"Course with id: '{courseId}' already has a topic with id: '{topicId}'.");

            throw new BusinessException($"Course with id: '{courseId}' already has a topic with id: '{topicId}'.");
        }

        await repository.AddTopicAsync(courseId, topicId);

        logger.LogInformation($"Topic with id: '{topicId}' successfully added to a course with id: '{courseId}'.");
    }
}
