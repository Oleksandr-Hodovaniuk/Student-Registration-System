using Application.DTOs;

namespace Application.Services.Interfaces;

public interface ITopicService
{
    public Task<IEnumerable<TopicDTO>> GetAllAsync();
    public Task CreateAsync(TopicDTO topic);
    public Task UpdateAsync(TopicDTO topic);
    public Task DeleteAsync(int id);
}
