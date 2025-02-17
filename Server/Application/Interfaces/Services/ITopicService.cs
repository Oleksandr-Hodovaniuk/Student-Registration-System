using Application.DTOs.Topic;

namespace Application.Interfaces.Services;

public interface ITopicService : IGenericService<TopicDTO, TopicCreateDTO, TopicCreateDTO>
{
}
