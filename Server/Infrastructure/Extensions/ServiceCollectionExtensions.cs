﻿using Application.Mappings;
using Application.Services.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;
using Application.Repositories;
using Infrastructure.Repositories;
using Application.Seeders;
using FluentValidation;
using Application.DTOs;
using Application.Validators;
using Infrastructure.Seeders;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Application.Mappers;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //Connecting to a database.
        var connectionString = configuration.GetConnectionString("StudentRegistrationSystemDb");
        services.AddDbContext<StudentRegistrationSystemDbContext>(options => options.UseSqlServer(connectionString));

        //Registration of logger.
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            loggingBuilder.AddNLog("nlog.config");
        });

        //Registration of repositories.
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ITopicRepository, TopicRepository>();

        //Registration of mappers.
        services.AddAutoMapper(typeof(UserProfile).Assembly);
        services.AddAutoMapper(typeof(CourseProfile).Assembly);
        services.AddAutoMapper(typeof(TopicProfile).Assembly);

        //Registration of seeder.
        services.AddScoped<ISeeder, DataSeeder>();

        //Registration of validators.
        services.AddScoped<IValidator<CreateUserDTO>, CreateUserDTOValidator>();
        services.AddScoped<IValidator<UpdateUserDTO>, UpdateUserDTOValidator>();
        services.AddScoped<IValidator<CreateCourseDTO>, CreateCourseDTOValidator>();
        services.AddScoped<IValidator<UpdateCourseDTO>, UpdateCourseDTOValidator>();
        services.AddScoped<IValidator<CreateTopicDTO>, CreateTopicDTOValidator>();
        services.AddScoped<IValidator<UpdateTopicDTO>, UpdateTopicDTOValidator>();

        //Registration of identity.
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<StudentRegistrationSystemDbContext>();

        //Registration of services.
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ITopicService, TopicService>();
    }
}
