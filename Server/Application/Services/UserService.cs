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
    public async Task<IEnumerable<UserCourseDTO>> GetAllAsync()
    {
        var users = await repository.GetAllAsync();

        if (!users.Any())
        {
            logger.LogWarning("Users don't exist.");

            return Enumerable.Empty<UserCourseDTO>();
        }

        logger.LogInformation("Users successfully returned.");

        return mapper.Map<IEnumerable<UserCourseDTO>>(users);
    }
    public async Task CreateAsync(UserCourseDTO dto)
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

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }



    public Task UpdateAsync(UserCourseDTO t)
    {
        throw new NotImplementedException();
    }
}
