﻿using Application.DTOs;
using AutoMapper;
using Core.Entities;

namespace Application.Mappings;

public class TopicProfile : Profile
{
    public TopicProfile()
    {
        CreateMap<Topic, TopicDTO>().ReverseMap();
    }
}
