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

    public CoursesController(ICourseService service, IValidator<CourseDTO> validator)
    {
        _service = service;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var courses = await _service.GetAllAsync();
            return Ok(courses);
        }
        catch (NotFoundException ex) 
        {
            return BadRequest(ex.Message);
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
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
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
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
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
    }
}
