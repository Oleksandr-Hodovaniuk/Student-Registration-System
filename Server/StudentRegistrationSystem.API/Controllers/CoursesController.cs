using Application.DTOs;
using Application.Exceptions;
using Application.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace StudentRegistrationSystem.API.Controllers;
[Route("api/courses")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _service;
    private readonly IValidator<CreateCourseDTO> _createValidator;
    private readonly IValidator<UpdateCourseDTO> _updateValidator;
    private readonly ILogger<CoursesController> _logger;

    public CoursesController(ICourseService service,
        IValidator<CreateCourseDTO> createValidator,
        IValidator<UpdateCourseDTO> updateValidator,
        ILogger<CoursesController> logger)
    {
        _service = service;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
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
    public async Task<IActionResult> GetAllByTopicsAsync([FromQuery] params int[] topicsIds)
    {
        try
        {
            var courses = await _service.GetAllByTopicsAsync(topicsIds);
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
    public async Task<IActionResult> CreateAsync([FromBody] CreateCourseDTO course)
    {
        var validationResult = await _createValidator.ValidateAsync(course);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning($"Course: '{course.Name}' failed validation.");

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
        catch (NotFoundException ex)
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
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateCourseDTO dto)
    {
        var validationResult = await _updateValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        try
        {
            await _service.UpdateAsync(dto);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while updating course.");

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

    [HttpPut("{courseId}/topics/{topicId}")]
    public async Task<IActionResult> AddTopicAsync(int courseId, int topicId)
    {
        try
        {
            await _service.AddTopicAsync(courseId, topicId);
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
            _logger.LogError(ex, "An unexpected error occurred while deleting course.");

            return StatusCode(500, "Internal server error.");
        }
    }
}
