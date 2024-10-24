using Core.Entities;
using Infrastructure.Persistence.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

internal class TopicSeeder(StudentRegistrationSystemDbContext dbcontext) : ISeeder
{
    public async Task SeedAsync()
    {
        if (await dbcontext.Database.CanConnectAsync())
        {
            if (!await dbcontext.Topics.AnyAsync())
            {
                var topics = GetTopics();
                await dbcontext.Topics.AddRangeAsync(topics);
                await dbcontext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Topic> GetTopics()
    {
        List<Topic> topics = [
            new()
            {
                Name = "C#"
            },

            new()
            {
                Name = "PHP"
            },

            new()
            {
                Name = "Java"
            }, 
            
            new()
            {
                Name = "C++"
            },
            new()
            {
                Name = "Python"
            }
        ];

        return topics;
    }
}
