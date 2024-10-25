using Application.Repositories;
using Core.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly StudentRegistrationSystemDbContext _context;
    public CourseRepository(StudentRegistrationSystemDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetAllAsync(int topicId)
    {
        return await _context.Courses
            .Where(c => c.Topics.Any(t => t.Id == topicId))
            .Include(c => c.Topics)
            .ToListAsync();
    }

    public async Task<Course?> GetAsync(int id)
    {
        return await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
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
