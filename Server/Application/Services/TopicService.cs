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
        return _mapper.Map<IEnumerable<TopicDTO>>(topics);
    }

    public async Task CreateAsync(TopicDTO topic)
    {
        if (await _repository.ExistsByNameAsync(topic.Name))
        {
            throw new BusinessException("Topic with this name already exists.");
        }

        var entity = _mapper.Map<Topic>(topic);
        await _repository.CreateAsync(entity);
    }

    public async Task UpdateAsync(TopicDTO topic)
    {
        var entity = _mapper.Map<Topic>(topic);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
