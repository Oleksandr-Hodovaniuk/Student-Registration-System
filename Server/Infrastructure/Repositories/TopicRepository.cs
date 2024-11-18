using Core.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Repositories;

namespace Infrastructure.Repositories;

internal class TopicRepository(StudentRegistrationSystemDbContext context) : ITopicRepository
{
    /// <summary>
    /// Returns all topics from a database.
    /// </summary>
    /// <returns>A collection of topics entities sorted by name.</returns>
    public async Task<IEnumerable<Topic>> GetAllAsync()
    {
        return await context.Topics.OrderBy(t => t.Name).ToListAsync();
    }

    /// <summary>
    /// Returns a topic by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the topic entity.</param>
    /// <returns>The topic entity.</returns>
    public async Task<Topic?> GetByIdAsync(int id)
    {
        return await context.Topics.FindAsync(id);
    }

    /// <summary>
    /// Creates a new topic in a database.
    /// </summary>
    /// <param name="topic">The topic entity that is added to a database.</param>
    /// <returns></returns>
    public async Task CreateAsync(Topic topic)
    {
        await context.Topics.AddAsync(topic);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates an existing entity in a database.
    /// </summary>
    /// <param name="topic">The topic entity that is updated in a database.</param>
    /// <returns></returns>
    public async Task UpdateAsync(Topic topic)
    {
        context.Topics.Update(topic);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a topic by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the topic entity.</param>
    /// <returns></returns>
    public async Task DeleteAsync(int id)
    {
        var topic = await context.Topics.FindAsync(id);

        context.Topics.Remove(topic!);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Checks if a topic exists in a database by its name.
    /// </summary>
    /// <param name="name">The name of the topic entity.</param>
    /// <returns>
    /// A boolean value: true if a topic with the specified name exists; otherwise, false.
    /// </returns>
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await context.Topics.AnyAsync(t => t.Name == name);
    }

    /// <summary>
    /// Checks if a topic exists in a database by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the topic entity.</param>
    /// <returns>
    /// A boolean value: true if a topic with the specified identifier exists; otherwise, false.
    /// </returns>
    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await context.Topics.AnyAsync(t => t.Id == id);
    }
}
