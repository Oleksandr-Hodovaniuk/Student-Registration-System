using Core.Entities;

namespace Application.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Returns all users.
    /// </summary>
    /// <returns>A collection of users entities.</returns>
    public Task<IEnumerable<User>> GetAllAsync();

    /// <summary>
    /// Returns a user by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the user entity.</param>
    /// <returns>The course entity.</returns>
    public Task<User?> GetByIdAsync(string id);

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user entity that is added to a database.</param>
    /// <returns></returns>
    public Task CreateAsync(User user);

    /// <summary>
    /// Updates an existing user entity in a database.
    /// </summary>
    /// <param name="user">The user entity that is updated in a database.</param>
    /// <returns></returns>
    public Task UpdateAsync(User user);

    /// <summary>
    /// Deletes a user by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the user entity.</param>
    /// <returns></returns>
    public Task DeleteAsync(string id);

    /// <summary>
    /// Adds a course to a course.
    /// </summary>
    /// <param name="userId">The identifier of the user entity.</param>
    /// <param name="courseId">The identifier of the course entity.</param>
    /// <returns></returns>
    public Task AddCourseAsync(string userId, int courseId);

    /// <summary>
    /// Removes a course from a user.
    /// </summary>
    /// <param name="userId">The identifier of the user entity.</param>
    /// <param name="courseId">The identifier of the course entity.</param>
    /// <returns></returns>
    public Task RemoveCourseAsync(string userId, int courseId);

    /// <summary>
    /// Checks if a user email exists.
    /// </summary>
    /// <param name="email">The email of the user entity.</param>
    /// <returns>
    /// A boolean value: true if specified email exists; otherwise, false.
    /// </returns>
    public Task<bool> ExistsByEmailAsync(string email);

    /// <summary>
    /// Checks if a user exists by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the user entity.</param>
    /// <returns>
    /// A boolean value: true if a user with the specified identifier exists; otherwise, false.
    /// </returns>
    public Task<bool> ExistsByIdAsync(string id);

    /// <summary>
    /// Checks if a course exists by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the course entity.</param>
    /// <returns>
    /// A boolean value: true if a course with the specified identifier exists; otherwise, false.
    /// </returns>
    public Task<bool> CourseExistsByIdAsync(int id);
}
