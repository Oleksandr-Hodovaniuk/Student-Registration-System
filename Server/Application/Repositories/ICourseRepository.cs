using Core.Entities;

namespace Application.Repositories;

public interface ICourseRepository
{
    public Task<IEnumerable<Course>> GetAllAsync();
    public Task<IEnumerable<Course>> GetAllAsync(int topicId);
    public Task<Course?> GetAsync(int id);
    public Task CreateAsync(Course course);
    public Task UpdateAsync(Course course);
    public Task DeleteAsync(int id);
}
