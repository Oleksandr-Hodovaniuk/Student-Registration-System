using Application.DTOs;
using Core.Entities;

namespace Application.Services.Interfaces;

public interface IUserService 
{
    public Task<IEnumerable<UserCoursesDTO>> GetAllAsync();
    public Task<UserCoursesDTO> GetByIdAsync(string id);
    public Task CreateAsync(CreateUserDTO dto);
    public Task UpdateAsync(UserCoursesDTO dto);
    public Task DeleteAsync(string id);
}
