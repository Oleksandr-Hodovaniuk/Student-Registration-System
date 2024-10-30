using Application.DTOs;
using Application.Exceptions;
using Application.Repositories;
using Application.Services.Interfaces;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CourseService : ICourseService
{
    private readonly IMapper _mapper;
    private readonly ICourseRepository _repository;
    private readonly ILogger<CourseService> _logger;

    public CourseService(ICourseRepository repository, IMapper mapper, ILogger<CourseService> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<CourseDTO>> GetAllAsync()
    {
        var courses = await _repository.GetAllAsync();

        if (!courses.Any())
        {
            _logger.LogWarning("Courses don't exist.");

            return Enumerable.Empty<CourseDTO>();
        }

        _logger.LogInformation("Courses successfully returned.");

        return _mapper.Map<IEnumerable<CourseDTO>>(courses);
    }

    public async Task<IEnumerable<CourseDTO>> GetAllByIdAsync( params int[] topicsId)
    {
        var courses = await _repository.GetAllByIdAsync(topicsId);

        if (!courses.Any())
        {
            _logger.LogWarning("Courses with these topics don't exist.");

            return Enumerable.Empty<CourseDTO>();
        }

        _logger.LogInformation("Courses successfully returned.");

        return _mapper.Map<IEnumerable<CourseDTO>>(courses);
    }

    public async Task<CourseDTO> GetByIdAsync(int id)
    {
        if (!await _repository.ExistsByIdAsync(id))
        {
            _logger.LogError($"Course with Id: '{id}' doesn't exist.");

            throw new NotFoundException($"Course with Id '{id}' doesn't exist.");
        }

        var course = await _repository.GetByIdAsync(id);

        _logger.LogInformation($"Course with Id: '{id}' successfully returned.");

        return _mapper.Map<CourseDTO>(course);
    }

    public async Task CreateAsync(CourseDTO dto)
    {
        if (dto.Id != 0)
        {
            _logger.LogError("Field: 'id' must be 0.");

            throw new BusinessException("Field: 'id' must be 0.");
        }
        if (await _repository.ExistsByNameAsync(dto.Name))
        {
            _logger.LogWarning($"Course with name: '{dto.Name}' already exists.");

            throw new BusinessException($"Course with name '{dto.Name}' already exists.");
        }

        var topics = await _repository.TopicsExistsAsync(dto.Topics);

        if (!topics.Any())
        {
            _logger.LogError($"These topics don't exist.");

            throw new NotFoundException($"These topics don't exist.");
        }
        
        var course = _mapper.Map<Course>(dto);
        //course.Topics = topics;

        await _repository.CreateAsync(course);

        _logger.LogInformation($"Course: '{course.Name}' successfully created.");
    }

    public async Task UpdateAsync(CourseDTO dto)
    {
        if (!await _repository.ExistsByIdAsync(dto.Id))
        {
            _logger.LogError($"Course with Id: '{dto.Id}' doesn't exist.");

            throw new NotFoundException($"Course with Id '{dto.Id}' doesn't exist.");     
        }
        if (await _repository.ExistsByNameAsync(dto.Name))
        {
            _logger.LogWarning($"Course with name: '{dto.Name}' already exists.");

            throw new BusinessException($"Course with name '{dto.Name}' already exists.");
        }

        var course = _mapper.Map<Course>(dto);
        await _repository.UpdateAsync(course);

        _logger.LogInformation($"Course with Id: '{course.Id}' successfully updated.");
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _repository.ExistsByIdAsync(id))
        {
            _logger.LogError($"Course with Id: '{id}' doesn't exist.");

            throw new NotFoundException($"Course with Id '{id}' doesn't exist.");
        }

        await _repository.DeleteAsync(id);

        _logger.LogInformation($"Course with Id: '{id}' successfully deleted.");
    }  
}
