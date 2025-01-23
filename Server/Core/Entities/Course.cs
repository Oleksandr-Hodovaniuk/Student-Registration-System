namespace Core.Entities;

public class Course
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime Beginning { get; set; }
    public short Duration { get; set; }
    public List<Topic> Topics { get; set; } = new();
}
