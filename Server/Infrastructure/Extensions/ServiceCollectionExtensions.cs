using Application.Mappings;
using Application.Services.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;
using Application.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Persistence.Seeders.Interfaces;
using Infrastructure.Persistence.Seeders;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {   
        //Connecting to a database.
        var connectionString = configuration.GetConnectionString("StudentRegistrationSystemDb");
        services.AddDbContext<StudentRegistrationSystemDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<ISeeder, CourseSeeder>();

        services.AddScoped<ISeeder, TopicSeeder>();

        services.AddAutoMapper(typeof(CourseProfile).Assembly);

        services.AddAutoMapper(typeof(TopicProfile).Assembly);

        services.AddScoped<ICourseRepository, CourseRepository>();

        services.AddScoped<ITopicRepository, TopicRepository>();

        services.AddScoped<ICourseService, CourseService>();

        services.AddScoped<ITopicService, TopicService>();
    }
}
