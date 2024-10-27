using Application.DTOs;
using Application.Exceptions;
using Application.Repositories;
using Application.Services.Interfaces;
using AutoMapper;
using Core.Entities;

namespace Application.Services;

public class TopicService : ITopicService
{
    private readonly IMapper _mapper;
    private readonly ITopicRepository _repository;

    public TopicService(ITopicRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<TopicDTO>> GetAllAsync()
    {
        var topics = await _repository.GetAllAsync();

        if (topics.Count() == 0)
        {      
            throw new NotFoundException("Topics are not exist.");
        }
        
        return _mapper.Map<IEnumerable<TopicDTO>>(topics);
    }

    public async Task CreateAsync(TopicDTO topic)
    {
        if (await _repository.ExistsByNameAsync(topic.Name))
        {
            throw new BusinessException($"Topic with name {topic.Name} already exists.");
        }

        var entity = _mapper.Map<Topic>(topic);
        await _repository.CreateAsync(entity);
    }

    public async Task UpdateAsync(TopicDTO topic)
    {
        if (!await _repository.ExistsByIdAsync(topic.Id))
        {
            throw new NotFoundException($"Topic with Id {topic.Id} doesn't exist.");
        }

        var entity = _mapper.Map<Topic>(topic);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _repository.ExistsByIdAsync(id))
        {
            throw new NotFoundException($"Topic with Id {id} doesn't exist.");
        }
        
        await _repository.DeleteAsync(id);
    }
}
