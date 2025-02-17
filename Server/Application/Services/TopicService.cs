using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Core.Entities;

namespace Application.Services;

public class TopicService(ITopicRepository repository) : ITopicService
{
    public async Task<TopicDTO> CreateAsync(TopicCreateDTO dto)
    {
        var topic = new Topic { Name = dto.Name };

        topic = await repository.CreateAsync(topic);

        var newDto = new TopicDTO { Id = topic.Id, Name = topic.Name };

        return newDto;
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TopicDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TopicDTO> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<TopicDTO> UpdateAsync(Guid id, TopicCreateDTO entity)
    {
        throw new NotImplementedException();
    }
}
