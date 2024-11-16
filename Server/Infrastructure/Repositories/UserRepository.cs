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

    public async Task<User?> GetByIdAsync(string id)
    {
        return await context.Users
            .Include(u => u.UserCourses.OrderBy(uc => uc.Course.Name))
            .ThenInclude(uc => uc.Course)
            .FirstOrDefaultAsync(u => u.Id == id);
            
    }

    public async Task CreateAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var user = await context.Users.FindAsync(id);

        context.Users.Remove(user!);
        await context.SaveChangesAsync();
    }

    public Task UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> ExistsByIdAsync(string id)
    {
        return await context.Users.AnyAsync(u => u.Id == id);
    }
}
