using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class UserCoursesConfiguration : IEntityTypeConfiguration<UserCourses>
{
    public void Configure(EntityTypeBuilder<UserCourses> builder)
    {
        builder.HasKey(uc => new { uc.UserId, uc.CourseId });

        builder.HasOne(uc => uc.User)
            .WithMany(u => u.UserCourses)
            .HasForeignKey(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(uc => uc.Course)
            .WithMany(c => c.UserCourses)
            .HasForeignKey(uc => uc.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
