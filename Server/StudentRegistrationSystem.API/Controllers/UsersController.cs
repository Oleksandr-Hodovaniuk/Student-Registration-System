using Application.DTOs;
using Application.Exceptions;
using Application.Services.Interfaces;
using Core.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace StudentRegistrationSystem.API.Controllers;
[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IValidator<UserCourseDTO> _validator;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService service, IValidator<UserCourseDTO> validator, ILogger<UsersController> logger)
    {
        _logger = logger;
        _validator = validator;
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var users = await _service.GetAllAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting courses.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(UserCourseDTO dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning($"User: '{dto.Name} {dto.LastName}' failed validation.");

            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        try
        {
            await _service.CreateAsync(dto);
            return Created();
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while creating course.");

            return StatusCode(500, "Internal server error.");
        }
    }
}
