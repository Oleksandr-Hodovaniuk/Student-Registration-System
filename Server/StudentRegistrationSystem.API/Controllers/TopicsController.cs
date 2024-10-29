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
    private readonly ILogger<TopicsController> _logger;
    public TopicsController(ITopicService service, IValidator<TopicDTO> validator, ILogger<TopicsController> logger)
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
            var topics = await _service.GetAllAsync();

            if (!topics.Any())
            {
                return NoContent();
            }

            return Ok(topics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting topic.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] TopicDTO topic)
    {
        var validationResult = await _validator.ValidateAsync(topic);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning($"Topic: '{topic.Name}' failed validation.");

            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        try
        {
            await _service.CreateAsync(topic);

            return StatusCode(201, new { message = "Topic created succesfully." });
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
            _logger.LogError(ex, "An unexpected error occurred while creating topic.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] TopicDTO topic)
    {
        var validationResult = await _validator.ValidateAsync(topic);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        try
        {
            await _service.UpdateAsync(topic);

            return NoContent();
        }
        catch(NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while updating topic.");

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
            _logger.LogError(ex, "An unexpected error occurred while deleting topic.");

            return StatusCode(500, "Internal server error.");
        }
    }
}
