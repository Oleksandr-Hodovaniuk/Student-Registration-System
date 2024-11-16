using Core.Entities;

namespace Application.Repositories;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllAsync();
    public Task<User?> GetByIdAsync(string id);
    public Task CreateAsync(User user);
    public Task UpdateAsync(User user);
    public Task DeleteAsync(string id);
    public Task<bool> ExistsByEmailAsync(string email);
    public Task<bool> ExistsByIdAsync(string id);

}
