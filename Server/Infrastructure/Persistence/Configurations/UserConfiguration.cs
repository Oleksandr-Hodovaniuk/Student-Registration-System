using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasMany(u => u.Courses)
            .WithMany(c => c.Users)
            .UsingEntity<UserCourse>(   //Configuring a junction table.
                j => j.HasOne(uc => uc.Course)
                      .WithMany(c => c.UserCourses)
                      .HasForeignKey(uc => uc.CourseId),
                j => j.HasOne(uc => uc.User)
                      .WithMany(u => u.UserCourses)
                      .HasForeignKey(uc => uc.UserId),
                j => j.HasKey(uc => new { uc.UserId, uc.CourseId })
            );
    }
}
