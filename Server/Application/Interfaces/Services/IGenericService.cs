namespace Application.Interfaces.Services;

public interface IGenericService<T, C, U> 
    where T : class 
    where C : class
    where U : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> CreateAsync(C entity);
    Task<T> UpdateAsync(Guid id, U entity);
    Task<bool> DeleteAsync(Guid id);
}
