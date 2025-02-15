using Application.Interfaces.Repositories;
using Core.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class TopicRepository(StudentRegistrationSystemDbContext dbContext) : ITopicRepository
    {
        public async Task<Topic> CreateAsync(Topic entity)
        {
            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var topic = await dbContext.Topcis.FindAsync(id);
            if (topic != null)
            {
                dbContext.Topcis.Remove(topic);
                await dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Topic>> GetAllAsync()
        {
            return await dbContext.Topcis.ToListAsync();
        }

        public async Task<Topic?> GetByIdAsync(Guid id)
        {
            return await dbContext.Topcis.FindAsync(id);
        }

        public async Task<Topic> UpdateAsync(Topic entity)
        {
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await dbContext.Topcis.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsByStringAsync(string str)
        {
            return await dbContext.Topcis.AnyAsync(x => x.Name == str);
        }
    }
}
