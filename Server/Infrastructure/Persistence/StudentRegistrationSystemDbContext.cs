using Core.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class StudentRegistrationSystemDbContext(DbContextOptions<StudentRegistrationSystemDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Topic> Topcis { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration of entities.
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
    }
}
