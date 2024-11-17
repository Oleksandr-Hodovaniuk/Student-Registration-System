using Application.DTOs;
using AutoMapper;
using Core.Entities;

namespace Application.Mappings;

public class TopicProfile : Profile
{
    public TopicProfile()
    {
        CreateMap<CreateTopicDTO, Topic>().ReverseMap();

        CreateMap<UpdateTopicDTO, Topic>().ReverseMap();

        CreateMap<Topic, TopicDTO>().ReverseMap();   
    }
}
