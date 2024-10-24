using Core.Entities;

namespace Infrastructure.Interfaces;

public interface ITopicRepository
{
    public Task<IEnumerable<Topic>> GetAllAsync();
    public Task CreateAsync(Topic topic);
    public Task UpdateAsync(Topic topic);
    public Task DeleteAsync(int id);
}
