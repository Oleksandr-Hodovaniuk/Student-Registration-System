using Application.DTOs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace StudentRegistrationSystem.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _service;

    public CoursesController(ICourseService service)
    {
        _service = service;
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
        if (!ModelState.IsValid || course == null || course.Id != 0)
        {
            return BadRequest(ModelState);
        }

        await _service.CreateAsync(course);

        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] CourseDTO course)
    {
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
