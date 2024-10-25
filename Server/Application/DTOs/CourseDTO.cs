using Core.Entities;

namespace Application.DTOs;

public class CourseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsAvailable { get; set; }
    public DateTime Beginning { get; set; }
    public short Duration { get; set; }
    public IEnumerable<TopicDTO> Topics { get; set; } = default!;
}
