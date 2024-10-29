using Application.DTOs;
using Application.Exceptions;
using Application.Repositories;
using Application.Services.Interfaces;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class TopicService : ITopicService
{
    private readonly IMapper _mapper;
    private readonly ITopicRepository _repository;
    private readonly ILogger<TopicService> _logger;

    public TopicService(ITopicRepository repository, IMapper mapper, ILogger<TopicService> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<TopicDTO>> GetAllAsync()
    {
        var topics = await _repository.GetAllAsync();

        if (!topics.Any())
        {
            _logger.LogWarning("Topics don't exist.");

            return Enumerable.Empty<TopicDTO>();
        }

        _logger.LogInformation("Topics successfully returned.");

        return _mapper.Map<IEnumerable<TopicDTO>>(topics);
    }

    public async Task CreateAsync(TopicDTO dto)
    {
        if (await _repository.ExistsByNameAsync(dto.Name))
        {
            _logger.LogWarning($"Topic with name: '{dto.Name}' already exists.");

            throw new BusinessException($"Topic with name '{dto.Name}' already exists.");
        }

        var topic = _mapper.Map<Topic>(dto);
        await _repository.CreateAsync(topic);

        _logger.LogInformation($"Topic: '{topic.Name}' successfully created.");
    }

    public async Task UpdateAsync(TopicDTO dto)
    {
        if (!await _repository.ExistsByIdAsync(dto.Id))
        {
            _logger.LogError($"Topic with Id: '{dto.Id}' doesn't exist.");

            throw new NotFoundException($"Topic with Id: '{dto.Id}' doesn't exist.");
        }
        if (await _repository.ExistsByNameAsync(dto.Name))
        {
            _logger.LogWarning($"Topic with name: '{dto.Name}' already exists.");

            throw new BusinessException($"Topic with name '{dto.Name}' already exists.");
        }

        var topic = _mapper.Map<Topic>(dto);
        await _repository.UpdateAsync(topic);

        _logger.LogInformation($"Topic with Id: '{topic.Id}' successfully updated.");
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _repository.ExistsByIdAsync(id))
        {
            _logger.LogError($"Topic with Id: '{id}' doesn't exist.");

            throw new NotFoundException($"Topic with Id: '{id}' doesn't exist.");
        }
        
        await _repository.DeleteAsync(id);

        _logger.LogInformation($"Topic with Id: '{id}' successfully deleted.");
    }
}
