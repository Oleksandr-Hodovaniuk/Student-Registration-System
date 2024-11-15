using Application.DTOs;

namespace Application.Services.Interfaces;

public interface ICourseService : IGenericService<CourseDTO, int>
{
    public Task<IEnumerable<CourseDTO>> GetAllByIdAsync(params int[] topicId);
}
