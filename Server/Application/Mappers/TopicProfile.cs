using Application.DTOs;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers;

public class TopicProfile : Profile
{
	public TopicProfile()
	{
		CreateMap<Topic, TopicDTO>().ReverseMap();
		CreateMap<Topic, TopicCreateDTO>().ReverseMap();
	}
}
