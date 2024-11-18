using Application.DTOs;
using Core.Entities;

namespace Application.Repositories;

public interface ICourseRepository
{
    public Task<IEnumerable<Course>> GetAllAsync();
    public Task<IEnumerable<Course>> GetAllByTopicsIdsAsync(params int[] topicsIds);
    public Task<Course?> GetByIdAsync(int id);
    public Task CreateAsync(Course dto);
    public Task UpdateAsync(Course dto);
    public Task DeleteAsync(int id);
    public Task AddTopicAsync(int courseId, int topicId);
    public Task RemoveTopicAsync(int courseId, int topicId);
    public Task<bool> ExistsByIdAsync(int id);
    public Task<bool> ExistsByNameAsync(string name);
    public Task<List<Topic>> TopicsExistsByIdsAsync(IEnumerable<TopicDTO> topics);
    public Task<bool> TopicExistsByIdAsync(int id);
}
