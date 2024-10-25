using Application.DTOs;
using Core.Entities;

namespace Application.Services.Interfaces;

public interface ICourseService
{
    public Task<IEnumerable<CourseDTO>> GetAllAsync();
    public Task<IEnumerable<CourseDTO>> GetAllAsync(int topicId);
    public Task<CourseDTO> GetAsync(int id);
    public Task CreateAsync(CourseDTO course);
    public Task UpdateAsync(CourseDTO course);
    public Task DeleteAsync(int id);
}
