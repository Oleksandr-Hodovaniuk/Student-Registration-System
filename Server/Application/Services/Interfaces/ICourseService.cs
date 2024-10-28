using Application.DTOs;

namespace Application.Services.Interfaces;

public interface ICourseService : IGenericService<CourseDTO>
{
    public Task<IEnumerable<CourseDTO>> GetAllByIdAsync(params int[] topicId);
}
