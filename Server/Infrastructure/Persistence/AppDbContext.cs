using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    internal class AppDbContext : DbContext
    {
        internal DbSet<Course> Courses { get; set; }
        internal DbSet<Topic> Topics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=XEONSILA;Database=StudentRegistrationSystemDb;Integrated Security=True;Trust Server Certificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Topics)
                .WithMany(c => c.Courses)
                .UsingEntity<Dictionary<string, object>>(   //Configuring the join table.
                    "CourseTopic",

                    e => e.HasOne<Topic>().WithMany().HasForeignKey("TopicId"),
                    e => e.HasOne<Course>().WithMany().HasForeignKey("CourseId"),
                    e => e.HasKey("CourseId", "TopicId")
                );
        }
    }
}
