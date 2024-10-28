using Application.DTOs;

namespace Application.Services.Interfaces;

public interface ICourseService : IService<CourseDTO>
{
    public Task<IEnumerable<CourseDTO>> GetAllByIdAsync(params int[] topicId);
}
