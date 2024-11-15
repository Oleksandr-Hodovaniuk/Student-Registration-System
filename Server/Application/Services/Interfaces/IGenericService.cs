using Application.DTOs;

namespace Application.Services.Interfaces;

public interface IGenericService<T, P> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T> GetByIdAsync(P id) => throw new NotImplementedException();
    public Task CreateAsync(T t);
    public Task UpdateAsync(T t);
    public Task DeleteAsync(P id);
}
