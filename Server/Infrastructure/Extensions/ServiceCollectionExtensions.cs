using Application.Mappings;
using Application.Services.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;
using Application.Repositories;
using Infrastructure.Repositories;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {   
        //Connecting to a database.
        var connectionString = configuration.GetConnectionString("StudentRegistrationSystemDb");
        services.AddDbContext<StudentRegistrationSystemDbContext>(options => options.UseSqlServer(connectionString));

        services.AddAutoMapper(typeof(TopicProfile).Assembly);

        services.AddScoped<ITopicRepository, TopicRepository>();

        services.AddScoped<ITopicService, TopicService>();
    }
}
