using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Core.Entities;

namespace Application.Services;

public class TopicService(ITopicRepository repository, IMapper mapper) : ITopicService
{
    public async Task<TopicDTO> CreateAsync(TopicCreateDTO dto)
    {
        var topic = mapper.Map<Topic>(dto);
        topic = await repository.CreateAsync(topic);

        return mapper.Map<TopicDTO>(topic);
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
