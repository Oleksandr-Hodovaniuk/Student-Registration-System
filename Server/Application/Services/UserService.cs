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

    public async Task UpdateAsync(UpdateUserDTO dto)
    {
        var user = await repository.GetByIdAsync(dto.Id);

        if (user == null)
        {
            logger.LogWarning($"User with id: '{dto.Id}' doesn't exist.");

            throw new NotFoundException($"User with id: '{dto.Id}' doesn't exist.");
        }

        if (user.Email != dto.Email && await repository.ExistsByEmailAsync(dto.Email!))
        {
            logger.LogWarning($"Email: '{dto.Email}' is already taken by another user.");

            throw new BusinessException($"Email: '{dto.Email}' is already taken by another user.");
        }   

        if (user.Name != dto.Name || user.LastName != dto.LastName || user.Age != dto.Age)
        {
            mapper.Map(dto, user);

            await repository.UpdateAsync(user);

            logger.LogInformation($"User with id: '{user.Id}' successfully updated.");
        }
        else
        {
            logger.LogInformation($"User with id: '{user.Id}' has no data to update.");
        }           
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

    public async Task AddCourseAsync(string userId, int courseId)
    {
        if (!await repository.ExistsByIdAsync(userId))
        {
            logger.LogError($"User with id: '{userId}' doesn't exist.");

            throw new NotFoundException($"User with id: '{userId}' doesn't exist.");
        }

        if (!await repository.CourseExistsByIdAsync(courseId))
        {
            logger.LogError($"Course with id: '{userId}' doesn't exist.");

            throw new NotFoundException($"Course with id: '{userId}' doesn't exist.");
        }

        var user = await repository.GetByIdAsync(userId);

        if (user!.UserCourses.Any(uc => uc.CourseId == courseId))
        {
            logger.LogError($"User with id: '{userId}' already has a course with id: '{courseId}'.");

            throw new BusinessException($"User with id: '{userId}' already has a course with id: '{courseId}'.");
        }

        await repository.AddCourseAsync(userId, courseId);

        logger.LogInformation($"Course with id: '{courseId}' successfully added to a user with id: '{userId}'.");
    }

    public async Task RemoveCourseAsync(string userId, int courseId)
    {
        if (!await repository.ExistsByIdAsync(userId))
        {
            logger.LogError($"User with id: '{userId}' doesn't exist.");

            throw new NotFoundException($"User with id: '{userId}' doesn't exist.");
        }

        if (await repository.CourseExistsByIdAsync(courseId))
        {
            logger.LogError($"Course with id: '{userId}' doesn't exist.");

            throw new NotFoundException($"Course with id: '{userId}' doesn't exist.");
        }

        var user = await repository.GetByIdAsync(userId);

        if (!user!.UserCourses.Any(uc => uc.CourseId == courseId))
        {
            logger.LogError($"User with id: '{userId}' doesn't have a course with id: '{courseId}'.");

            throw new BusinessException($"User with id: '{userId}' doesn't have a course with id: '{courseId}'.");
        }

        await repository.AddCourseAsync(userId, courseId);

        logger.LogInformation($"Course with id: '{courseId}' successfully remowed to a user with id: '{userId}'.");
    }
}
