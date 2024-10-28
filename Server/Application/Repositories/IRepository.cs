namespace Application.Repositories;

//Base repository interface with base methods.
public interface IRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T?> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task CreateAsync(T t);
    public Task UpdateAsync(T t);
    public Task DeleteAsync(int id);
    public Task<bool> ExistsByIdAsync(int id);
    public Task<bool> ExistsByNameAsync(string name) => throw new NotImplementedException();
}
