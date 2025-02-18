using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Core.Entities;

namespace Application.Services;

public class TopicService(ITopicRepository repository, IMapper mapper) : ITopicService
{
    public async Task<TopicDTO> CreateAsync(TopicCreateDTO dto)
    {
        if (await repository.ExistsByNameAsync(dto.Name))
        {
            throw new BusinessException($"Topic with name: {dto.Name} already exists.");
        }

        var topic = mapper.Map<Topic>(dto);
        topic = await repository.CreateAsync(topic);

        return mapper.Map<TopicDTO>(topic);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        if (!await repository.ExistsByIdAsync(id))
        {
            throw new NotFoundException($"Topic with id: {id} doesn't exist.");
        }

        return await repository.DeleteAsync(id);
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
