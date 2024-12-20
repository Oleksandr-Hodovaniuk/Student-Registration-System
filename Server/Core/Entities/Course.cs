﻿namespace Core.Entities;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Author { get; set; } = default!;
    public DateTime СreationDate { get; set; }
    public DateTime Beginning { get; set; }
    public short Duration { get; set; }
    public List<Topic> Topics { get; set; } = new();
    public List<User> Users { get; set; } = new();
    public List<UserCourses> UserCourses { get; set; } = new();
}
