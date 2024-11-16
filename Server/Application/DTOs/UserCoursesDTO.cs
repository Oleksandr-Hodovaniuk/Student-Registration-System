using Core.Models;

namespace Application.DTOs;

public class UserCoursesDTO
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public int Age { get; set; }
    public string Email { get; set; } = default!;
    public List<CourseData>? Courses { get; set; } = new();
}