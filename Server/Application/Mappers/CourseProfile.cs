using Application.DTOs;
using AutoMapper;
using Core.Entities;

namespace Application.Mappings;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseDTO>().ReverseMap();
    }
}
