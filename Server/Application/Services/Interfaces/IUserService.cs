﻿using Application.DTOs;
using Core.Entities;

namespace Application.Services.Interfaces;

public interface IUserService 
{
    public Task<IEnumerable<UserCoursesDTO>> GetAllAsync();
    public Task<UserCoursesDTO> GetByIdAsync(string id);
    public Task CreateAsync(CreateUserDTO dto);
    public Task UpdateAsync(UpdateUserDTO dto);
    public Task DeleteAsync(string id);
    public Task AddCourseAsync(string userId, int courseId);
    public Task RemoveCourseAsync(string userId, int courseId);
}
