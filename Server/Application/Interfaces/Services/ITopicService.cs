using Application.DTOs;

namespace Application.Interfaces.Services;

public interface ITopicService : IGenericService<TopicDTO, TopicCreateDTO, TopicCreateDTO>
{
}
