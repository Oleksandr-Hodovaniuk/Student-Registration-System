using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;
using Application.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Seeders;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Connection to a database.
        var connectionString = configuration.GetConnectionString("StudentRegistrationSystemDb");
        services.AddDbContext<StudentRegistrationSystemDbContext>(options => options.UseSqlServer(connectionString));

        // Registration of DataSeeder.
        services.AddTransient<DefaultDataSeeder>();

        // Registration of repositories.
        services.AddScoped<ITopicRepository, TopicRepository>();

        // Registration of mappers.
        services.AddAutoMapper(typeof(TopicProfile).Assembly);

        // Registration of services.
        services.AddScoped<ITopicService, TopicService>();
    }
}
