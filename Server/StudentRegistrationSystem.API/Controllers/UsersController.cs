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
    private readonly IValidator<CreateUserDTO> _createValidator;
    private readonly IValidator<UpdateUserDTO> _updateValidator;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService service,
        IValidator<CreateUserDTO> createValidator,
        IValidator<UpdateUserDTO> updateValidator,
        ILogger<UsersController> logger)
    {
        _logger = logger;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        try
        {
            var user = await _service.GetByIdAsync(id);
            return Ok(user);
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting courses.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateUserDTO dto)
    {
        var validationResult = await _createValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning($"User: '{dto.Name} {dto.LastName}' failed validation.");

            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        try
        {
            await _service.CreateAsync(dto);
            return StatusCode(201, new { message = "User created succesfully." });
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

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateUserDTO dto)
    {
        var validationResult = await _updateValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning($"User: '{dto.Name} {dto.LastName}' failed validation.");

            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        try
        {
            await _service.UpdateAsync(dto);
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
            _logger.LogError(ex, "An unexpected error occurred while creating course.");

            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while deleting course.");

            return StatusCode(500, "Internal server error.");
        }
    }
}
