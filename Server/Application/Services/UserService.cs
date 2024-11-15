using Application.DTOs;
using Application.Repositories;
using Application.Services.Interfaces;
using AutoMapper;
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
    public Task CreateAsync(UserCourseDTO t)
    {
        throw new NotImplementedException();
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
