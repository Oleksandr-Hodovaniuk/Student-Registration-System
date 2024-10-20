using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //An extension method to add services to the container and connect to a database.
        public static void AddInfractructure(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("StudentRegistrationSystemDb"); //Get connection string from a JSON file.

            services.AddDbContext<StudentRegistrationSystemDbContext>(options => 
                options.UseSqlServer(connectionString));
        }
    }
}
