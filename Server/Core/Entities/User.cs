using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class User : IdentityUser
{
    public string Name { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public int Age { get; set; }
    public List<Course> Courses  => UserCourses.Select(uc => uc.Course).ToList();
    public List<UserCourse> UserCourses { get; set; } = new();
}
