using Application.DTOs;
using Application.Exceptions;
using Application.Repositories;
using Application.Services.Interfaces;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CourseService(ICourseRepository repository, IMapper mapper, ILogger<CourseService> logger) : ICourseService
{
    public async Task<IEnumerable<CourseDTO>> GetAllAsync()
    {
        var courses = await repository.GetAllAsync();

        if (!courses.Any())
        {
            logger.LogWarning("Courses do not exist.");

            return Enumerable.Empty<CourseDTO>();
        }

        logger.LogInformation("Courses successfully returned.");

        return mapper.Map<IEnumerable<CourseDTO>>(courses);
    }

    public async Task<IEnumerable<CourseDTO>> GetAllByTopicsIdsAsync( params int[] topicsIds)
    {
        var courses = await repository.GetAllByTopicsIdsAsync(topicsIds);

        if (!courses.Any())
        {
            logger.LogWarning("Courses with these topics do not exist.");

            return Enumerable.Empty<CourseDTO>();
        }

        logger.LogInformation("Courses successfully returned.");

        return mapper.Map<IEnumerable<CourseDTO>>(courses);
    }

    public async Task<CourseDTO> GetByIdAsync(int id)
    {
        if (!await repository.ExistsByIdAsync(id))
        {
            logger.LogError($"Course with id: '{id}' does not exist.");

            throw new NotFoundException($"Course with id '{id}' does not exist.");
        }

        var course = await repository.GetByIdAsync(id);

        logger.LogInformation($"Course with id: '{id}' successfully returned.");

        return mapper.Map<CourseDTO>(course);
    }

    public async Task CreateAsync(CreateCourseDTO dto)
    {
        if (await repository.ExistsByNameAsync(dto.Name))
        {
            logger.LogWarning($"Course with name: '{dto.Name}' already exists.");

            throw new BusinessException($"Course with name '{dto.Name}' already exists.");
        }

        var topics = await repository.TopicsExistByIdsAsync(dto.Topics);

        if (!topics.Any())
        {
            logger.LogError($"One or more topics do not exist.");

            throw new NotFoundException($"One or more topics do not exist.");
        }
        
        var course = mapper.Map<Course>(dto);
        course.Topics = topics;

        await repository.CreateAsync(course);

        logger.LogInformation($"Course: '{course.Name}' successfully created.");
    }

    public async Task UpdateAsync(UpdateCourseDTO dto)
    {
        if (!await repository.ExistsByIdAsync(dto.Id))
        {
            logger.LogError($"Course with id: '{dto.Id}' does not exist.");

            throw new NotFoundException($"Course with id '{dto.Id}' does not exist.");     
        }

        var course = mapper.Map<Course>(dto);
        await repository.UpdateAsync(course);

        logger.LogInformation($"Course with id: '{course.Id}' successfully updated.");
    }

    public async Task DeleteAsync(int id)
    {
        if (!await repository.ExistsByIdAsync(id))
        {
            logger.LogError($"Course with id: '{id}' does not exist.");

            throw new NotFoundException($"Course with id '{id}' does not exist.");
        }

        await repository.DeleteAsync(id);

        logger.LogInformation($"Course with id: '{id}' successfully deleted.");
    }

    public async Task AddTopicAsync(int courseId, int topicId)
    {
        if (!await repository.ExistsByIdAsync(courseId))
        {
            logger.LogError($"Course with id: '{courseId}' does not exist.");

            throw new NotFoundException($"Course with id '{courseId}' does not exist.");
        }

        if (!await repository.TopicExistsByIdAsync(topicId))
        {
            logger.LogError($"Topic with id: '{topicId}' does not exist.");

            throw new NotFoundException($"Topic with id '{topicId}' does not exist.");
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

    public async Task RemoveTopicAsync(int courseId, int topicId)
    {
        if (!await repository.ExistsByIdAsync(courseId))
        {
            logger.LogError($"Course with id: '{courseId}' does not exist.");

            throw new NotFoundException($"Course with id '{courseId}' does not exist.");
        }

        if (!await repository.TopicExistsByIdAsync(topicId))
        {
            logger.LogError($"Topic with id: '{topicId}' does not exist.");

            throw new NotFoundException($"Topic with id '{topicId}' does not exist.");
        }

        var course = await repository.GetByIdAsync(courseId);

        if (!course!.Topics.Any(t => t.Id == topicId))
        {
            logger.LogError($"Course with id: '{courseId}' does not have a topic with id: '{topicId}'.");

            throw new BusinessException($"Course with id: '{courseId}' does not have a topic with id: '{topicId}'.");
        }

        await repository.RemoveTopicAsync(courseId, topicId);

        logger.LogInformation($"Topic with id: '{topicId}' successfully removed from a course with id: '{courseId}'.");
    }
}
