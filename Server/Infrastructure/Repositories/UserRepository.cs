using Application.Repositories;
using Core.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class UserRepository(StudentRegistrationSystemDbContext context) : IUserRepository
{
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await context.Users
            .Include(u => u.UserCourses)
            .ThenInclude(uc => uc.Course)
            .OrderBy(u => u.Name)
            .ToListAsync();
    }

    public Task CreateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    

    public Task UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }
}
