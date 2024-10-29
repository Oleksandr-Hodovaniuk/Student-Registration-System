using Application.DTOs;
using Core.Entities;

namespace Application.Repositories;

public interface ICourseRepository : IGenericRepository<Course>
{
    public Task<IEnumerable<Course>> GetAllByIdAsync(params int[] topicsId);

    public Task<IEnumerable<Topic>> TopicsExistsAsync(IEnumerable<TopicDTO> topics);
}
