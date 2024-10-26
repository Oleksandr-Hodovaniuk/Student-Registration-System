using Application.Mappings;
using Application.Services.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;
using Application.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Persistence.Seeders;
using System.Data;
using Application.Seeders;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {   
        //Connecting to a database.
        var connectionString = configuration.GetConnectionString("StudentRegistrationSystemDb");
        services.AddDbContext<StudentRegistrationSystemDbContext>(options => options.UseSqlServer(connectionString));

        // Registration of repositories.
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ITopicRepository, TopicRepository>();

        //Registration of mappers.
        services.AddAutoMapper(typeof(CourseProfile).Assembly);
        services.AddAutoMapper(typeof(TopicProfile).Assembly);

        //Registration of seeders.
        services.AddScoped<IDataSeeder, CourseSeeder>();
        services.AddScoped<IDataSeeder, TopicSeeder>();

        //Registration of services.
        services.AddScoped<IDataSeederService, DataSeederService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ITopicService, TopicService>();
    }
}
