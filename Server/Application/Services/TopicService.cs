using Application.DTOs;
using Application.Exceptions;
using Application.Repositories;
using Application.Services.Interfaces;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class TopicService(ITopicRepository repository, IMapper mapper, ILogger<TopicService> logger) : ITopicService
{
    public async Task<IEnumerable<TopicDTO>> GetAllAsync()
    {
        var topics = await repository.GetAllAsync();

        if (!topics.Any())
        {
            logger.LogWarning("Topics don't exist.");

            return Enumerable.Empty<TopicDTO>();
        }

        logger.LogInformation("Topics successfully returned.");

        return mapper.Map<IEnumerable<TopicDTO>>(topics);
    }

    public async Task CreateAsync(TopicDTO dto)
    {
        if (await repository.ExistsByNameAsync(dto.Name))
        {
            logger.LogWarning($"Topic with name: '{dto.Name}' already exists.");

            throw new BusinessException($"Topic with name '{dto.Name}' already exists.");
        }

        var topic = mapper.Map<Topic>(dto);
        await repository.CreateAsync(topic);

        logger.LogInformation($"Topic: '{topic.Name}' successfully created.");
    }

    public async Task UpdateAsync(TopicDTO dto)
    {
        if (!await repository.ExistsByIdAsync(dto.Id))
        {
            logger.LogError($"Topic with id: '{dto.Id}' doesn't exist.");

            throw new NotFoundException($"Topic with id: '{dto.Id}' doesn't exist.");
        }
        if (await repository.ExistsByNameAsync(dto.Name))
        {
            logger.LogWarning($"Topic with name: '{dto.Name}' already exists.");

            throw new BusinessException($"Topic with name '{dto.Name}' already exists.");
        }

        var topic = mapper.Map<Topic>(dto);
        await repository.UpdateAsync(topic);

        logger.LogInformation($"Topic with id: '{topic.Id}' successfully updated.");
    }

    public async Task DeleteAsync(int id)
    {
        if (!await repository.ExistsByIdAsync(id))
        {
            logger.LogError($"Topic with id: '{id}' doesn't exist.");

            throw new NotFoundException($"Topic with id: '{id}' doesn't exist.");
        }
        
        await repository.DeleteAsync(id);

        logger.LogInformation($"Topic with id: '{id}' successfully deleted.");
    }
}
