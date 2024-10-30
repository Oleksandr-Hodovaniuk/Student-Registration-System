using Application.DTOs;
using Application.Repositories;
using Core.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class CourseRepository(StudentRegistrationSystemDbContext context) : ICourseRepository
{
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await context.Courses
            .OrderBy(c => c.Name)
            .Include(c => c.Topics)
            .ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetAllByIdAsync(params int[] topicsId)
    {
        return await context.Courses
            .Where(c => topicsId.All(id => c.Topics.Any(t => t.Id == id)))   //Returns all courses that have all topics from topicId. 
            .Include(c => c.Topics)
            .ToListAsync();
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        return await context.Courses
            .Include(c => c.Topics)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task CreateAsync(Course course)
    {
        await context.Courses.AddAsync(course);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Course course)
    {
        context.Courses.Update(course);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var course = await context.Courses.FindAsync(id);

        context.Courses.Remove(course!);
        await context.SaveChangesAsync();
    }
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await context.Courses.AnyAsync(t => t.Name == name);
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await context.Courses.AnyAsync(t => t.Id == id);
    }

    public async Task<List<Topic>> TopicsExistsAsync(IEnumerable<TopicDTO> topics)
    {
        var topicsIds = topics.Select(t => t.Id).ToList();  //Gets all ids from TopicDTOs.
        var existingIds = await context.Topics     //Gets ids fropm Topics table that match the topicsIds.
            .Where(t => topicsIds.Contains(t.Id))
            .Select(t => t.Id)
            .ToListAsync();

        if (!topicsIds.All(id => existingIds.Contains(id))) //If some of ids don't match the existingIds - return empty collection.
        {
            return new List<Topic>();
        }
 
        return await context.Topics.Where(t => existingIds.Contains(t.Id)).ToListAsync();  
    }
}
