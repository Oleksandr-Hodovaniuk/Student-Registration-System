namespace Application.DTOs;

public class UpdateCourseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Author { get; set; } = default!;
    public DateTime СreationDate { get; set; } = default!;
    public DateTime Beginning { get; set; } = default!;
    public short Duration { get; set; }
}
