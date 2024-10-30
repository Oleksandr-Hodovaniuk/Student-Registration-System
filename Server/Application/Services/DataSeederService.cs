using Application.Seeders;
using Application.Services.Interfaces;

namespace Application.Services;

public class DataSeederService : IDataSeederService
{
    private readonly IEnumerable<IDataSeeder> _seeders;

    public DataSeederService(IEnumerable<IDataSeeder> seeders)
    {
        _seeders = seeders;
    }

    public async Task SeedAsync()
    {
        foreach (var seeder in _seeders)
        {
            await seeder.SeedAsync();
        }
    }
}
