using Application.DTOs;
using Core.Entities;

namespace Application.Repositories;

public interface ICourseRepository : IGenericRepository<Course, int>
{
    public Task<IEnumerable<Course>> GetAllByIdAsync(params int[] topicsId);

    public Task<List<Topic>> TopicsExistsAsync(IEnumerable<TopicDTO> topics);
}
