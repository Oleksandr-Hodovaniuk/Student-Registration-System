using Core.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TopicRepository : ITopicRepository
{
    private readonly StudentRegistrationSystemDbContext _context;

    public TopicRepository(StudentRegistrationSystemDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Topic>> GetAllAsync()
    {
        return await _context.Topics.OrderBy(t => t.Name).ToListAsync();
    }
    public async Task CreateAsync(Topic topic)
    {
        await _context.Topics.AddAsync(topic);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Topic topic)
    {
        _context.Topics.Update(topic);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var topic = await _context.Topics.FindAsync(id);

        _context.Topics.Remove(topic!);
        await _context.SaveChangesAsync();
    }
}
