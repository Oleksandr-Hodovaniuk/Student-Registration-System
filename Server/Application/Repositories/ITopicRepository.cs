using Core.Entities;

namespace Application.Repositories;

public interface ITopicRepository
{
    /// <summary>
    /// Returns all topics.
    /// </summary>
    /// <returns>A collection of topics entities sorted by name.</returns>
    
    public Task<IEnumerable<Topic>> GetAllAsync();
    /// <summary>
    /// Returns a topic by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the topic entity.</param>
    /// <returns>A topic entity.</returns>
    public Task<Topic?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new topic.
    /// </summary>
    /// <param name="topic">The topic entity that is added to a database.</param>
    /// <returns></returns>
    public Task CreateAsync(Topic topic);

    /// <summary>
    /// Updates an existing topic entity in a database.
    /// </summary>
    /// <param name="topic">The topic entity that is updated in a database.</param>
    /// <returns></returns>
    public Task UpdateAsync(Topic topic);

    /// <summary>
    /// Deletes a topic by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the topic entity.</param>
    /// <returns></returns>
    public Task DeleteAsync(int id);

    /// <summary>
    /// Checks if a topic exists by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the topic entity.</param>
    /// <returns>
    /// A boolean value: true if a topic with the specified identifier exists; otherwise, false.
    /// </returns>
    public Task<bool> ExistsByIdAsync(int id);

    /// <summary>
    /// Checks if a topic exists by its name.
    /// </summary>
    /// <param name="name">The name of the topic entity.</param>
    /// <returns>
    /// A boolean value: true if a topic with the specified name exists; otherwise, false.
    /// </returns>
    public Task<bool> ExistsByNameAsync(string name);
}
