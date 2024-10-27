using Application.Repositories;
using Core.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class CourseRepository : ICourseRepository
{
    private readonly StudentRegistrationSystemDbContext _context;
    public CourseRepository(StudentRegistrationSystemDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses
            .OrderBy(c => c.Name)
            .Include(c => c.Topics)
            .ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetAllByIdAsync(params int[] topicId)
    {
        return await _context.Courses
            .Where(c => topicId.All(id => c.Topics.Any(t => t.Id == id)))   //Returns all courses that have all topics with topicId. 
            .Include(c => c.Topics)
            .ToListAsync();
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.Topics)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task CreateAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var course = await _context.Courses.FindAsync(id);

        _context.Courses.Remove(course!);
        await _context.SaveChangesAsync();
    }
}
