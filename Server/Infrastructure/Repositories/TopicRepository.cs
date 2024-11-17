using Core.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Repositories;

namespace Infrastructure.Repositories;

internal class TopicRepository(StudentRegistrationSystemDbContext context) : ITopicRepository
{
    public async Task<IEnumerable<Topic>> GetAllAsync()
    {
        return await context.Topics.OrderBy(t => t.Name).ToListAsync();
    }

    public async Task<Topic?> GetByIdAsync(int id)
    {
        return await context.Topics.FindAsync(id);
    }

    public async Task CreateAsync(Topic topic)
    {
        await context.Topics.AddAsync(topic);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Topic topic)
    {
        context.Topics.Update(topic);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var topic = await context.Topics.FindAsync(id);

        context.Topics.Remove(topic!);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await context.Topics.AnyAsync(t => t.Name == name);
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await context.Topics.AnyAsync(t => t.Id == id);
    }
}
