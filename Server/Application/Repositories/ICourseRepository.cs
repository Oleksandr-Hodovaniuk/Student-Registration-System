using Core.Entities;

namespace Application.Repositories;

public interface ICourseRepository
{
    /// <summary>
    /// Returns all courses from a database.
    /// </summary>
    /// <returns>A collection of courses entities sorted by name.</returns>
    public Task<IEnumerable<Course>> GetAllAsync();
    
    /// <summary>
    /// Returns all courses that has specific topics from a database.
    /// </summary>
    /// <param name="topicsId">The array of topics identifiers.</param>
    /// <returns>Returns a collection of courses that has specific topics from a database.</returns>
    public Task<IEnumerable<Course>> GetAllByTopicsIdsAsync(params int[] topicsIds);
    
    /// <summary>
    /// Returns a course by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the course entity.</param>
    /// <returns>The course entity.</returns>
    public Task<Course?> GetByIdAsync(int id);
    
    /// <summary>
    /// Creates a new course in a database.
    /// </summary>
    /// <param name="course">The course entity that is added to a database.</param>
    /// <returns></returns>
    public Task CreateAsync(Course dto);
    
    /// <summary>
    /// Updates an existing course entity in a database.
    /// </summary>
    /// <param name="course">The course entity that is updated in a database.</param>
    /// <returns></returns>
    public Task UpdateAsync(Course dto);
    
    /// <summary>
    /// Deletes a course by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the course entity.</param>
    /// <returns></returns>
    public Task DeleteAsync(int id);
    
    /// <summary>
    /// Adds a topic to a course.
    /// </summary>
    /// <param name="courseId">The identifier of the course entity.</param>
    /// <param name="topicId">The identifier of the topic entity.</param>
    /// <returns></returns>
    public Task AddTopicAsync(int courseId, int topicId);
    
    /// <summary>
    /// Removes a topic from a course.
    /// </summary>
    /// <param name="courseId">The identifier of the course entity.</param>
    /// <param name="topicId">The identifier of the topic entity.</param>
    /// <returns></returns>
    public Task RemoveTopicAsync(int courseId, int topicId);
   
    /// <summary>
    /// Checks if a course exists in a database by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the course entity.</param>
    /// <returns>
    /// A boolean value: true if a course with the specified identifier exists; otherwise, false.
    /// </returns>
    public Task<bool> ExistsByIdAsync(int id);
    
    /// <summary>
    /// Checks if a course exists in a database by its name.
    /// </summary>
    /// <param name="name">The name of the course entity.</param>
    /// <returns>
    /// A boolean value: true if a course with the specified name exists; otherwise, false.
    /// </returns>
    public Task<bool> ExistsByNameAsync(string name);
    
    /// <summary>
    /// Returns all topics if they exist in a database.
    /// </summary>
    /// <param name="topics">The TopicDTO collection.</param>
    /// <returns>
    /// A Topic collection value: Topic collection if all topics exist that have transmitted identifiers;
    /// otherwise, empty Topic collection.
    /// </returns>
    public Task<List<Topic>> TopicsExistByIdsAsync(List<int> topics);
    
    /// <summary>
    /// Checks if a topic exists in a database by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the topic entity.</param>
    /// <returns>
    /// A boolean value: true if a topic with the specified identifier exists; otherwise, false.
    /// </returns>
    public Task<bool> TopicExistsByIdAsync(int id);
}
