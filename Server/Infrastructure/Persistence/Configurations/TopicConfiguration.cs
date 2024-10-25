using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasMany(t => t.Courses)
            .WithMany(c => c.Topics)
            .UsingEntity<Dictionary<string, object>>(   //Configuring a junction table.
                "CourseTopic",
                j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId"),
                j => j.HasOne<Topic>().WithMany().HasForeignKey("TopicId"),
                j => j.HasKey("CourseId", "TopicId")
            );
    }
}
