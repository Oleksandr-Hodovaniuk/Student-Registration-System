using Core.Entities;

namespace Application.Repositories;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllAsync();
    public Task<User?> GetByIdAsync(string id);
    public Task CreateAsync(User user);
    public Task UpdateAsync(User user);
    public Task DeleteAsync(string id);
    public Task AddCourseAsync(string userId, int courseId);
    public Task RemoveCourseAsync(string userId, int courseId);
    public Task<bool> ExistsByEmailAsync(string email);
    public Task<bool> ExistsByIdAsync(string id);
    public Task<bool> CourseExistsByIdAsync(int id);

}
