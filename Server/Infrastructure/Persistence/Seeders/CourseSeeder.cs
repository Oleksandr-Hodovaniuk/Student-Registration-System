using Core.Entities;
using Infrastructure.Persistence.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

internal class CourseSeeder(StudentRegistrationSystemDbContext dbContext) : ISeeder
{
    public async Task SeedAsync()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!await dbContext.Courses.AnyAsync())
            {
                var topics = GetCourses();
                dbContext.Courses.AddRange(topics);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Course> GetCourses()
    {
        List<Course> courses = [
            new() 
            {
               Name = "CodeQuest",
               Description = "An intensive training course that combines" +
                " the fundamentals of programming with modern approaches " +
                "to software development. Throughout the course, you will" +
                " master key programming principles, learn to work with " +
                "various languages such as C#, JavaScript, and Python, and" +
                " create real-world projects using frameworks and libraries." +
                " The course will help you gain practical skills and prepare" +
                " you for a professional path in the IT industry.",
               IsAvailable = true,
               Beginning = DateTime.Parse("2024-10-25 14:30:00.1234567"),
               Duration = 56
            },
        ];

        return courses;
    }
}
