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
   
    public async Task<IEnumerable<Course>> GetAllByTopicsIdsAsync(params int[] topicsIds)
    {
        return await context.Courses
            .Where(c => topicsIds.All(id => c.Topics.Any(t => t.Id == id)))
            .OrderBy(c => c.Name)
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
  
    public async Task AddTopicAsync(int courseId, int topicId)
    {
        var topic = await context.Topics.FindAsync(topicId);
        var course = await context.Courses.FindAsync(courseId);
        course?.Topics.Add(topic!);

        await context.SaveChangesAsync();
    }
   
    public async Task RemoveTopicAsync(int courseId, int topicId)
    {
        var topic = await context.Topics.FindAsync(topicId);
        var course = await context.Courses.FindAsync(courseId);
        course?.Topics.Remove(topic!);

        await context.SaveChangesAsync();
    }
    
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await context.Courses.AnyAsync(c => c.Name == name);
    }
 
    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await context.Courses.AnyAsync(c => c.Id == id);
    }

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

    public async Task<bool> TopicExistsByIdAsync(int id)
    {
        return await context.Topics.AnyAsync(t => t.Id == id);
    }
}
