using Application.DTOs;
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

    public CoursesController(ICourseService service, IValidator<CourseDTO> validator)
    {
        _service = service;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var courses = await _service.GetAllAsync();

        if (courses == null || !courses.Any())
        {
            return NotFound("No courses found.");
        }

        return Ok(courses);
    }

    [HttpGet("allById")]
    public async Task<IActionResult> GetAllByIdAsync([FromQuery] params int[] topicId)
    {
        var courses = await _service.GetAllByIdAsync(topicId);

        if (courses == null || !courses.Any())
        {
            return NotFound("No courses found.");
        }

        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var course =  await _service.GetByIdAsync(id);
        return Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CourseDTO course)
    {
        var validationResult = await _validator.ValidateAsync(course);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select( e => e.ErrorMessage));
        }
       
        await _service.CreateAsync(course);
        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] CourseDTO course)
    {
        var validationResult = await _validator.ValidateAsync(course);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        await _service.UpdateAsync(course);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
