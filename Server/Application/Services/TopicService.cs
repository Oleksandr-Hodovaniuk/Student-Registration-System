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
        if(await repository.ExistsByNameAsync(dto.Name))
        {
            throw new BusinessException($"Topic with name: {dto.Name} already exists.");
        }

        var topic = mapper.Map<Topic>(dto);
        topic = await repository.CreateAsync(topic);

        return mapper.Map<TopicDTO>(topic);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        if(!await repository.ExistsByIdAsync(id))
        {
            throw new NotFoundException($"Topic with id: {id} doesn't exist.");
        }

        return await repository.DeleteAsync(id);
    } 

    public async Task<IEnumerable<TopicDTO>> GetAllAsync()
    {
        var topics =  await repository.GetAllAsync();

        return mapper.Map<IEnumerable<TopicDTO>>(topics);
    }

    public async Task<TopicDTO> GetByIdAsync(Guid id)
    {
        if(!await repository.ExistsByIdAsync(id))
        {
            throw new NotFoundException($"Topic with id: {id} doesn't exist.");
        }

        var topic = await repository.GetByIdAsync(id);

        return mapper.Map<TopicDTO>(topic);
    }

    public async Task<TopicDTO> UpdateAsync(Guid id, TopicCreateDTO dto)
    {
        if (!await repository.ExistsByIdAsync(id))
        {
            throw new NotFoundException($"Topic with id: {id} doesn't exist.");
        }
        else if (await repository.ExistsByNameAsync(dto.Name))
        {
            throw new BusinessException($"Topic with name: {dto.Name} already exists.");
        }

        var topic = await repository.GetByIdAsync(id);
        topic!.Name = dto.Name;

        await repository.UpdateAsync(topic);

        return mapper.Map<TopicDTO>(topic);
    }
}
