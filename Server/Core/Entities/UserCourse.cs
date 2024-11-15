namespace Core.Entities;

public class UserCourse
{
    public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;

    public int CourseId { get; set; }
    public Course Course { get; set; } = default!;
    public DateTime RegistrationDate { get; set; }
}
