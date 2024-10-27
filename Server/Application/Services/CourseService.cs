using Application.DTOs;
using Application.Exceptions;
using Application.Repositories;
using Application.Services.Interfaces;
using AutoMapper;
using Core.Entities;

namespace Application.Services;

public class CourseService : ICourseService
{
    private readonly IMapper _mapper;
    private readonly ICourseRepository _repository;

    public CourseService(ICourseRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<CourseDTO>> GetAllAsync()
    {
        var courses = await _repository.GetAllAsync();

        if (courses.Count() == 0)
        {
            throw new NotFoundException("Courses don't exist.");
        }

        return _mapper.Map<IEnumerable<CourseDTO>>(courses);
    }

    public async Task<IEnumerable<CourseDTO>> GetAllByIdAsync( params int[] topicId)
    {
        var courses = await _repository.GetAllByIdAsync(topicId);

        if (courses.Count() == 0)
        {
            throw new NotFoundException("Courses with these topics don't exist.");
        }

        return _mapper.Map<IEnumerable<CourseDTO>>(courses);
    }

    public async Task<CourseDTO> GetByIdAsync(int id)
    {
        if (!await _repository.ExistsByIdAsync(id))
        {
            throw new NotFoundException($"Course with Id {id} doesn't exist.");
        }

        var course = await _repository.GetByIdAsync(id);
        return _mapper.Map<CourseDTO>(course);
    }

    public async Task CreateAsync(CourseDTO course)
    {
        var entity = _mapper.Map<Course>(course);
        await _repository.CreateAsync(entity);
    }

    public async Task UpdateAsync(CourseDTO course)
    {
        if (!await _repository.ExistsByIdAsync(course.Id))
        {
            throw new NotFoundException($"Course with Id {course.Id} doesn't exist.");
        }

        var entity = _mapper.Map<Course>(course);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _repository.ExistsByIdAsync(id))
        {
            throw new NotFoundException($"Course with Id {id} doesn't exist.");
        }
        await _repository.DeleteAsync(id);
    }  
}
