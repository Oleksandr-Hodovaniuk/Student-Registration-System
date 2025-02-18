namespace Core.Entities;

public class Topic
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public List<Course>? Courses { get; set; } = new List<Course>();
}
