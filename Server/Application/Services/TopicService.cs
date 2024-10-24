using Application.DTOs;
using Application.Repositories;
using Application.Services.Interfaces;
using AutoMapper;

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
        throw new NotImplementedException();
    }
    public async Task CreateAsync(TopicDTO topic)
    {
        throw new NotImplementedException();
    }
    public async Task UpdateAsync(TopicDTO topic)
    {
        throw new NotImplementedException();
    }
    public async Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}
