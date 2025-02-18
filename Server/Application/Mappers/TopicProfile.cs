using Application.DTOs;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers;

public class TopicProfile : Profile
{
	public TopicProfile()
	{
		CreateMap<Topic, TopicDTO>().ReverseMap();

		CreateMap<TopicCreateDTO, Topic>()
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			.ForMember(dest => dest.Courses, opt => opt.Ignore());
	}
}
