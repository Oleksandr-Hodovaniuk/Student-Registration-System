using Application.DTOs;
using Application.Exceptions;
using Application.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace StudentRegistrationSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopicsController : ControllerBase
{
    private readonly ITopicService _service;
    private readonly IValidator<TopicDTO> _validator;
    public TopicsController(ITopicService service, IValidator<TopicDTO> validator)
    {
        _service = service;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var topics = await _service.GetAllAsync();
            return Ok(topics);
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] TopicDTO topic)
    {
        var validationResult = await _validator.ValidateAsync(topic);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        try
        {
            await _service.CreateAsync(topic);
            return StatusCode(201, new { message = "Topic created succesfully." });
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] TopicDTO topic)
    {
        try
        {
            await _service.UpdateAsync(topic);
            return NoContent();
        }
        catch(NotFoundException ex)
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
