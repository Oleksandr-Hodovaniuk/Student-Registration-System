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
            logger.LogWarning("Topics do not exist.");

            return Enumerable.Empty<TopicDTO>();
        }

        logger.LogInformation("Topics successfully returned.");

        return mapper.Map<IEnumerable<TopicDTO>>(topics);
    }

    public async Task CreateAsync(CreateTopicDTO dto)
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

    public async Task UpdateAsync(UpdateTopicDTO dto)
    {
        var topic = await repository.GetByIdAsync(dto.Id);

        if (topic == null)
        {
            logger.LogWarning($"Topic with id: '{dto.Id}' does not exist.");

            throw new NotFoundException($"Topic with id: '{dto.Id}' does not exist.");
        }

        if (dto.Name != topic.Name && await repository.ExistsByNameAsync(dto.Name))
        {
            logger.LogWarning($"Name: '{dto.Name}' is already taken by another user.");

            throw new BusinessException($"Name: '{dto.Name}' is already taken by another user.");
        }

        if (topic.Name != dto.Name)
        {
            mapper.Map(dto, topic);

            await repository.UpdateAsync(topic);

            logger.LogInformation($"Topic with id: '{topic.Id}' successfully updated.");
        }
        else
        {
            logger.LogInformation($"Topic with id: '{topic.Id}' has no data to update.");
        }
    }

    public async Task DeleteAsync(int id)
    {
        if (!await repository.ExistsByIdAsync(id))
        {
            logger.LogError($"Topic with id: '{id}' does not exist.");

            throw new NotFoundException($"Topic with id: '{id}' does not exist.");
        }
        
        await repository.DeleteAsync(id);

        logger.LogInformation($"Topic with id: '{id}' successfully deleted.");
    }
}
