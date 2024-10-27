using Application.DTOs;
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
        return _mapper.Map<IEnumerable<CourseDTO>>(courses);
    }

    public async Task<IEnumerable<CourseDTO>> GetAllByIdAsync( params int[] topicId)
    {
        var courses = await _repository.GetAllByIdAsync(topicId);
        return _mapper.Map<IEnumerable<CourseDTO>>(courses);
    }

    public async Task<CourseDTO> GetByIdAsync(int id)
    {
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
        var entity = _mapper.Map<Course>(course);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }  
}
