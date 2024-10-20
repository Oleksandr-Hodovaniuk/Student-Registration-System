using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //Adds services to IServiceCollection and connects to a database.
        public static void AddInfractructure(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("StudentRegistrationSystemDb"); //Gets a connection string from a JSON file.

            services.AddDbContext<StudentRegistrationSystemDbContext>(options => 
                options.UseSqlServer(connectionString));
        }
    }
}
