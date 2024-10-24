using Core.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

internal class StudentRegistrationSystemDbContext(DbContextOptions<StudentRegistrationSystemDbContext> options) 
    :DbContext(options)
{
    internal DbSet<Course> Courses { get; set; }
    internal DbSet<Topic> Topics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Configuring entities.
        modelBuilder.ApplyConfiguration(new TopicConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
    }
}
