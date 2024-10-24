﻿namespace Core.Entities;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsAvailable { get; set; }
    public DateTime Beginning { get; set; }
    public short Duration { get; set; }
    public IEnumerable<Topic> Topics { get; set; } = default!;
}
