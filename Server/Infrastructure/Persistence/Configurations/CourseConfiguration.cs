using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.Topics)
            .WithMany(t => t.Courses)
            .UsingEntity<Dictionary<string, object>>(   
                "CourseTopics",
                j => j.HasOne<Topic>()
                    .WithMany()
                    .HasForeignKey("TopicId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Course>()
                    .WithMany()
                    .HasForeignKey("CourseId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasKey("CourseId", "TopicId")
            );

        builder.HasMany(c => c.UserCourses)
            .WithOne(uc => uc.Course)
            .HasForeignKey(uc => uc.CourseId);
    }
}
