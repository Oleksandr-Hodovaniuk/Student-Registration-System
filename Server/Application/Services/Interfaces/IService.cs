using Application.DTOs;

namespace Application.Services.Interfaces;

public interface IService<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task CreateAsync(T t);
    public Task UpdateAsync(T t);
    public Task DeleteAsync(int id);
}
