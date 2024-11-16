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
            logger.LogWarning("Courses don't exist.");

            return Enumerable.Empty<CourseDTO>();
        }

        logger.LogInformation("Courses successfully returned.");

        return mapper.Map<IEnumerable<CourseDTO>>(courses);
    }

    public async Task<IEnumerable<CourseDTO>> GetAllByIdAsync( params int[] topicsId)
    {
        var courses = await repository.GetAllByIdAsync(topicsId);

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

    public async Task CreateAsync(CourseDTO dto)
    {
        if (dto.Id != 0)
        {
            logger.LogError("Field: 'id' must be 0.");

            throw new BusinessException("Field: 'id' must be 0.");
        }
        if (await repository.ExistsByNameAsync(dto.Name))
        {
            logger.LogWarning($"Course with name: '{dto.Name}' already exists.");

            throw new BusinessException($"Course with name '{dto.Name}' already exists.");
        }

        var topics = await repository.TopicsExistsAsync(dto.Topics);

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

    public async Task UpdateAsync(CourseDTO dto)
    {
        if (!await repository.ExistsByIdAsync(dto.Id))
        {
            logger.LogError($"Course with id: '{dto.Id}' doesn't exist.");

            throw new NotFoundException($"Course with id '{dto.Id}' doesn't exist.");     
        }
        if (await repository.ExistsByNameAsync(dto.Name))
        {
            logger.LogWarning($"Course with name: '{dto.Name}' already exists.");

            throw new BusinessException($"Course with name '{dto.Name}' already exists.");
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
}
