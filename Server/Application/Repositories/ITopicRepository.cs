using Core.Entities;

namespace Application.Repositories;

public interface ITopicRepository
{
    public Task<IEnumerable<Topic>> GetAllAsync();
    public Task CreateAsync(Topic topic);
    public Task UpdateAsync(Topic topic);
    public Task DeleteAsync(int id);
    public Task<bool> ExistsByNameAsync(string name);
    public Task<bool> ExistsByIdAsync(int id);
}
