using Application.Seeders;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

internal class TopicSeeder(StudentRegistrationSystemDbContext dbcontext) : IDataSeeder
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
            new() { Name = "C#"},
            new() { Name = "PHP"},
            new() { Name = "Java"}, 
            new() { Name = "C++"},
            new() { Name = "Python"},
            new() { Name = "JavaScript"},
            new() { Name = "HTML"},
            new() { Name = "CSS"},
            new() { Name = "SQL"},
            new() { Name = "Entity Framework"},
            new() { Name = "ADO.NET"},
            new() { Name = "WPF"},
            new() { Name = "OOP"},
            new() { Name = "Angular"},
            new() { Name = "React"}
        ];

        return topics;
    }
}
