using Application.Interfaces.Repositories;
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
    }
}
