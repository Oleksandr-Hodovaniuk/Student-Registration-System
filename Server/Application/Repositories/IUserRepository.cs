using Core.Entities;

namespace Application.Repositories;

public interface IUserRepository : IGenericRepository<User, string>
{
    public Task<bool> ExistsByEmailAsync(string email);
}
