using Core.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders
{
    internal class TopicSeeder(StudentRegistrationSystemDbContext dbContext) : ITopicSeeder
    {
        public async Task SeedAsync()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!await dbContext.Topics.AnyAsync())
                {
                    var topics = GetTopics();
                    dbContext.Topics.AddRange(topics);
                    await dbContext.SaveChangesAsync();
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
                   Name = "Java"
               },

               new()
               {
                   Name = "PHP"
               }

            ];

            return topics;
        }
    }
}
