namespace Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class 
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsByIdAsync(Guid id);
    Task<bool> ExistsByNameAsync(string str);
}
