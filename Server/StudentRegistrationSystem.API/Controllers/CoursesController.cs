using Application.DTOs;
using Application.Exceptions;
using Application.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace StudentRegistrationSystem.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _service;
    private readonly IValidator<CourseDTO> _validator;
    private readonly ILogger<CoursesController> _logger;

    public CoursesController(ICourseService service, IValidator<CourseDTO> validator, ILogger<CoursesController> logger)
    {
        _service = service;
        _validator = validator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var courses = await _service.GetAllAsync();
            return Ok(courses);
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "An unexpected error occurred while getting courses.");

            return StatusCode(500, "Internal server error.");
        }  
    }

    [HttpGet("allById")]
    public async Task<IActionResult> GetAllByIdAsync([FromQuery] params int[] topicId)
    {
        try
        {
            var courses = await _service.GetAllByIdAsync(topicId);
            return Ok(courses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting courses.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var course = await _service.GetByIdAsync(id);
            return Ok(course);
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting course.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CourseDTO course)
    {
        var validationResult = await _validator.ValidateAsync(course);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        try
        {
            await _service.CreateAsync(course);
            return Created();
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while updating course.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] CourseDTO course)
    {
        var validationResult = await _validator.ValidateAsync(course);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        try
        {
            await _service.UpdateAsync(course);
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
            _logger.LogError(ex, "An unexpected error occurred while upd topic.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
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
