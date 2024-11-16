using Application.DTOs;
using Application.Exceptions;
using Application.Repositories;
using Application.Services.Interfaces;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class UserService(IUserRepository repository, IMapper mapper, ILogger<CourseService> logger) : IUserService
{
    public async Task<IEnumerable<UserCoursesDTO>> GetAllAsync()
    {
        var users = await repository.GetAllAsync();

        if (!users.Any())
        {
            logger.LogWarning("Users don't exist.");

            return Enumerable.Empty<UserCoursesDTO>();
        }

        logger.LogInformation("Users successfully returned.");

        return mapper.Map<IEnumerable<UserCoursesDTO>>(users);
    }
    public async Task<UserCoursesDTO> GetByIdAsync(string id)
    {
        if(!await repository.ExistsByIdAsync(id))
        {
            logger.LogWarning($"User with id: '{id}' doesn't exist.");

            throw new NotFoundException($"User with id: '{id}' doesn't exist.");
        }

        var user = await repository.GetByIdAsync(id);

        logger.LogInformation($"User with id: '{id}' successfully returned.");

        return mapper.Map<UserCoursesDTO>(user);
    }

    public async Task CreateAsync(CreateUserDTO dto)
    {
        if (await repository.ExistsByEmailAsync(dto.Email))
        {
            logger.LogWarning($"User with email: '{dto.Email}' already exists.");

            throw new BusinessException($"User with email '{dto.Email}' already exists.");
        }

        var user = mapper.Map<User>(dto);
        await repository.CreateAsync(user);

        logger.LogInformation($"User: '{user.Name} {user.LastName}' successfully created.");
    }

    public async Task DeleteAsync(string id)
    {
        if (!await repository.ExistsByIdAsync(id))
        {
            logger.LogWarning($"User with id: '{id}' doesn't exist.");

            throw new NotFoundException($"User with id: '{id}' doesn't exist.");
        }

        await repository.DeleteAsync(id);

        logger.LogInformation($"User with id: '{id}' successfully deleted.");
    }



    public Task UpdateAsync(UserCoursesDTO t)
    {
        throw new NotImplementedException();
    }
}
