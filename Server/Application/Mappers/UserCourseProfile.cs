using Application.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Models;

namespace Application.Mappers;

public class UserCourseProfile : Profile
{
    public UserCourseProfile()
    {
        CreateMap<User, UserCourseDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Courses, opt => opt.MapFrom(src =>
                src.UserCourses != null
                ? src.UserCourses.Select(uc => new CourseData
                {
                    Name = uc.Course.Name,
                    RegistrationDate = uc.RegistrationDate,
                    StudyDate = uc.Course.Beginning
                }).ToList()
                : new List<CourseData>()
            ));

        CreateMap<UserCourseDTO, User>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserCourses, opt => opt.MapFrom(src =>
                src.Courses != null
                ? src.Courses.Select(course => new UserCourse
                {
                    Course = new Course { Name = course.Name, Beginning = course.StudyDate },
                    RegistrationDate = course.RegistrationDate
                }).ToList()
                : new List<UserCourse>()
            ));
    }
}

