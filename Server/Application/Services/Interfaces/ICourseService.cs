using Application.DTOs;

namespace Application.Services.Interfaces;

public interface ICourseService
{
    public Task<IEnumerable<CourseDTO>> GetAllAsync();
    public Task<IEnumerable<CourseDTO>> GetAllByTopicsAsync(params int[] topicsIds);
    public Task<CourseDTO> GetByIdAsync(int id);
    public Task CreateAsync(CreateCourseDTO t);
    public Task UpdateAsync(UpdateCourseDTO t);
    public Task DeleteAsync(int id);
    public Task AddTopicAsync(int courseId, int topicId);
    public Task RemoveTopicAsync(int courseId, int topicId);
}
