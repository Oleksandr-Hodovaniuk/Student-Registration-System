using Application.DTOs;
using Application.Exceptions;
using Application.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace StudentRegistrationSystem.API.Controllers;
[Route("api/users")]
[ApiController]
public class UsersController(
    IUserService service,
    IValidator<CreateUserDTO> createValidator,
    IValidator<UpdateUserDTO> updateValidator,
    ILogger<UsersController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var users = await service.GetAllAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while getting courses.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        try
        {
            var user = await service.GetByIdAsync(id);
            return Ok(user);
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while getting courses.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserDTO dto)
    {
        var validationResult = await createValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            logger.LogWarning($"User: '{dto.Name} {dto.LastName}' failed validation.");

            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        try
        {
            await service.CreateAsync(dto);
            return StatusCode(201, new { message = "User created succesfully." });
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while creating course.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserDTO dto)
    {
        var validationResult = await updateValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            logger.LogWarning($"User: '{dto.Name} {dto.LastName}' failed validation.");

            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        try
        {
            await service.UpdateAsync(dto);
            return Created();
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while creating course.");

            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        try
        {
            await service.DeleteAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while deleting course.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPut("{userId}/courses/{courseId}")]
    public async Task<IActionResult> AddCourseAsync(string userId, [FromBody] int courseId)
    {
        try
        {
            await service.AddCourseAsync(userId, courseId);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while deleting course.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpDelete("{userId}/courses/{courseId}")]
    public async Task<IActionResult> RemoveCourseAsync(string userId, int courseId)
    {
        try
        {
            await service.RemoveCourseAsync(userId, courseId);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while deleting course.");

            return StatusCode(500, "Internal server error.");
        }
    }
}
