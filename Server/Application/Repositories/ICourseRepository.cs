using Core.Entities;

namespace Application.Repositories;

public interface ICourseRepository : IGenericRepository<Course>
{
    public Task<IEnumerable<Course>> GetAllByIdAsync(params int[] topicId);
}
