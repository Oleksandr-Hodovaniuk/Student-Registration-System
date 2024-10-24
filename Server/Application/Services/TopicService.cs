using Application.DTOs;
using Application.Repositories;
using Application.Services.Interfaces;

namespace Application.Services;

internal class TopicService : ITopicService
{
    private readonly ITopicRepository _repository;
    public TopicService(ITopicRepository repository)
    {
        _repository = repository;
    }
    public Task<IEnumerable<TopicDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
    public Task CreateAsync(TopicDTO topic)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    

    public Task UpdateAsync(TopicDTO topic)
    {
        throw new NotImplementedException();
    }
}
