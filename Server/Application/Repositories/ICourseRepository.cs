using Core.Entities;

namespace Application.Repositories;

public interface ICourseRepository
{
    public Task<IEnumerable<Course>> GetAllAsync();
    public Task<IEnumerable<Course>> GetAllByIdAsync(int topicId);
    public Task<Course?> GetByIdAsync(int id);
    public Task CreateAsync(Course course);
    public Task UpdateAsync(Course course);
    public Task DeleteAsync(int id);
}
