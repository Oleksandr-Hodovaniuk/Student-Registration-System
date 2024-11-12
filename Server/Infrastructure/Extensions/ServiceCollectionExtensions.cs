using Application.Mappings;
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
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ITopicRepository, TopicRepository>();

        //Registration of mappers.
        services.AddAutoMapper(typeof(CourseProfile).Assembly);
        services.AddAutoMapper(typeof(TopicProfile).Assembly);

        //Registration of seeder.
        services.AddScoped<ISeeder, DataSeeder>();

        //Registration of validators.
        services.AddScoped<IValidator<CourseDTO>, CourseDTOValidator>();
        services.AddScoped<IValidator<TopicDTO>, TopicDTOValidator>();

        //Registration of services.
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ITopicService, TopicService>();
    }
}
