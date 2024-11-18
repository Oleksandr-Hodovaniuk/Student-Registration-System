using Application.DTOs;
using Application.Exceptions;
using Application.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace StudentRegistrationSystem.API.Controllers;
[Route("api/courses")]
[ApiController]
public class CoursesController(
    ICourseService service,
    IValidator<CreateCourseDTO> createValidator,
    IValidator<UpdateCourseDTO> updateValidator,
    ILogger<CoursesController> logger) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var courses = await service.GetAllAsync();
            return Ok(courses);
        }
        catch (Exception ex) 
        {
            logger.LogError(ex, "An unexpected error occurred while getting courses.");

            return StatusCode(500, "Internal server error.");
        }  
    }

    [HttpGet("by-topics")]
    public async Task<IActionResult> GetAllByTopicsAsync([FromQuery] params int[] topicsIds)
    {
        try
        {
            var courses = await service.GetAllByTopicsIdsAsync(topicsIds);
            return Ok(courses);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while getting courses.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var course = await service.GetByIdAsync(id);
            return Ok(course);
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while getting course.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCourseDTO course)
    {
        var validationResult = await createValidator.ValidateAsync(course);

        if (!validationResult.IsValid)
        {
            logger.LogWarning($"Course: '{course.Name}' failed validation.");

            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        try
        {
            await service.CreateAsync(course);
            return StatusCode(201, new { message = "Course created succesfully." });
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
            logger.LogError(ex, "An unexpected error occurred while creating course.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateCourseDTO dto)
    {
        var validationResult = await updateValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        try
        {
            await service.UpdateAsync(dto);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while updating course.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
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

    [HttpPut("{courseId}/topics/{topicId}")]
    public async Task<IActionResult> AddTopicAsync(int courseId, int topicId)
    {
        try
        {
            await service.AddTopicAsync(courseId, topicId);
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

    [HttpDelete("{courseId}/topics/{topicId}")]
    public async Task<IActionResult> RemoveTopicAsync(int courseId, int topicId)
    {
        try
        {
            await service.RemoveTopicAsync(courseId, topicId);
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
