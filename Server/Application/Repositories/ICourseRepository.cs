using Core.Entities;

namespace Application.Repositories;

public interface ICourseRepository : IRepository<Course>
{
    public Task<IEnumerable<Course>> GetAllByIdAsync(params int[] topicId);
}
