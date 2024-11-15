namespace Application.Repositories;

//Base repository interface with base methods.
public interface IGenericRepository<T, P> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T?> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task CreateAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(int id);
    public Task<bool> ExistsByIdAsync(P id);
    public Task<bool> ExistsByNameAsync(string name);
}
