using Core.Entities;

namespace Application.Repositories;

public interface ITopicRepository : IRepository<Topic>
{
    public Task<bool> ExistsByNameAsync(string name);
}
