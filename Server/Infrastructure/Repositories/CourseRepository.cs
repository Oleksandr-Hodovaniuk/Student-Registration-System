using Application.DTOs;
using Application.Repositories;
using Core.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class CourseRepository(StudentRegistrationSystemDbContext context) : ICourseRepository
{
    /// <summary>
    /// Returns all courses from a database.
    /// </summary>
    /// <returns>A collection of courses entities sorted by name.</returns>
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await context.Courses
            .OrderBy(c => c.Name)
            .Include(c => c.Topics)
            .ToListAsync();
    }


    /// <summary>
    /// Returns all courses that has specific topics from a database.
    /// </summary>
    /// <param name="topicsId">The array of topics identifiers.</param>
    /// <returns>Returns a collection of courses that has specific topics from a database.</returns>
    public async Task<IEnumerable<Course>> GetAllByTopicsIdsAsync(params int[] topicsIds)
    {
        return await context.Courses
            .Where(c => topicsIds.All(id => c.Topics.Any(t => t.Id == id)))
            .OrderBy(c => c.Name)
            .Include(c => c.Topics)
            .ToListAsync();
    }


    /// <summary>
    /// Returns a course by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the course entity.</param>
    /// <returns>The course entity.</returns>
    public async Task<Course?> GetByIdAsync(int id)
    {
        return await context.Courses
            .Include(c => c.Topics)
            .FirstOrDefaultAsync(c => c.Id == id);
    }


    /// <summary>
    /// Creates a new course in a database.
    /// </summary>
    /// <param name="course">The course entity that is added to a database.</param>
    /// <returns></returns>
    public async Task CreateAsync(Course course)
    {
        await context.Courses.AddAsync(course);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates an existing course entity in a database.
    /// </summary>
    /// <param name="course">The course entity that is updated in a database.</param>
    /// <returns></returns>
    public async Task UpdateAsync(Course course)
    {
        context.Courses.Update(course);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a course by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the course entity.</param>
    /// <returns></returns>
    public async Task DeleteAsync(int id)
    {
        var course = await context.Courses.FindAsync(id);

        context.Courses.Remove(course!);
        await context.SaveChangesAsync();
    }


    /// <summary>
    /// Adds a topic to a course.
    /// </summary>
    /// <param name="courseId">The identifier of the course entity.</param>
    /// <param name="topicId">The identifier of the topic entity.</param>
    /// <returns></returns>
    public async Task AddTopicAsync(int courseId, int topicId)
    {
        var topic = await context.Topics.FindAsync(topicId);
        var course = await context.Courses.FindAsync(courseId);
        course?.Topics.Add(topic!);

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Removes a topic from a course.
    /// </summary>
    /// <param name="courseId">The identifier of the course entity.</param>
    /// <param name="topicId">The identifier of the topic entity.</param>
    /// <returns></returns>
    public async Task RemoveTopicAsync(int courseId, int topicId)
    {
        var topic = await context.Topics.FindAsync(topicId);
        var course = await context.Courses.FindAsync(courseId);
        course?.Topics.Remove(topic!);

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Checks if a course exists in a database by its name.
    /// </summary>
    /// <param name="name">The name of the course entity.</param>
    /// <returns>
    /// A boolean value: true if a course with the specified name exists; otherwise, false.
    /// </returns>
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await context.Courses.AnyAsync(c => c.Name == name);
    }

    /// <summary>
    /// Checks if a course exists in a database by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the course entity.</param>
    /// <returns>
    /// A boolean value: true if a course with the specified identifier exists; otherwise, false.
    /// </returns>
    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await context.Courses.AnyAsync(c => c.Id == id);
    }


    /// <summary>
    /// Returns all topics if they exist in a database.
    /// </summary>
    /// <param name="topics">The TopicDTO collection.</param>
    /// <returns>
    /// A TopicDTO collection value: TopicDTO collection if all topics exist that have transmitted identifiers;
    /// otherwise, empty TopicDTO collection.
    /// </returns>
    public async Task<List<Topic>> TopicsExistByIdsAsync(List<int> topicsIds)
    {
        //Gets all topics from a database that contains transmitted ids.
        var existingTopics = await context.Topics   
        .Where(t => topicsIds.Contains(t.Id))
        .ToListAsync();

        //Check whether the number of topics found matches the number of transmitted ids.
        if (existingTopics.Count != topicsIds.Count)
        {
            return new List<Topic>(); 
        }

        return existingTopics;
    }

    /// <summary>
    /// Checks if a topic exists in a database by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the topic entity.</param>
    /// <returns>
    /// A boolean value: true if a topic with the specified identifier exists; otherwise, false.
    /// </returns>
    public async Task<bool> TopicExistsByIdAsync(int id)
    {
        return await context.Topics.AnyAsync(t => t.Id == id);
    }
}
