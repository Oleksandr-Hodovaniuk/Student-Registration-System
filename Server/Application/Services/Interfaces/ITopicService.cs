using Application.DTOs;
using Core.Entities;

namespace Application.Services.Interfaces;

public interface ITopicService
{
    public Task<IEnumerable<TopicDTO>> GetAllAsync();
    public Task CreateAsync(CreateTopicDTO dto);
    public Task UpdateAsync(UpdateTopicDTO dto);
    public Task DeleteAsync(int id);
}
