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
        var topics = await _service.GetAllAsync();

        if (topics == null || !topics.Any())
        {
            return NotFound("No topics found.");
        }

        return Ok(topics);
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
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] TopicDTO topic)
    {
        if (id != topic.Id)
        {
            return BadRequest("ID in URL and body mismatch. Please provide the correct ID.");
        }

        await _service.UpdateAsync(topic);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}
