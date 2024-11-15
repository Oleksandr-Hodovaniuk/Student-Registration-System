namespace Application.DTOs;

public class CourseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Author { get; set; } = default!;
    public DateTime СreationDate { get; set; }
    public DateTime Beginning { get; set; }
    public short Duration { get; set; }
    public IEnumerable<TopicDTO> Topics { get; set; } = default!;
}
