using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.Topics)
            .WithMany(t => t.Courses)
            .UsingEntity<Dictionary<string, object>>(   //Configuring a junction table.
                "CourseTopic",
                j => j.HasOne<Topic>().WithMany().HasForeignKey("TopicId"),
                j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId"),
                j => j.HasKey("CourseId", "TopicId")
            );
    }
}
