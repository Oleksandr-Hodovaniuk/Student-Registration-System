using Application.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Models;

namespace Application.Mappings;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<CreateCourseDTO, Course>().ReverseMap();

        CreateMap<UpdateCourseDTO, Course>().ReverseMap();

        CreateMap<Course, CourseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.СreationDate, opt => opt.MapFrom(src => src.СreationDate.ToString("dd-MM-yyyy")))
            .ForMember(dest => dest.Beginning, opt => opt.MapFrom(src => src.Beginning.ToString("dd-MM-yyyy")))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
            .ForMember(dest => dest.Topics, opt => opt.MapFrom(src => src.Topics.Select(t => 
            new TopicDTO
            {
                Id = t.Id,
                Name = t.Name
            })));
    }
}
