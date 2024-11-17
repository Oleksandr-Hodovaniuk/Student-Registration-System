using Application.DTOs;
using Application.Exceptions;
using Application.Services.Interfaces;
using Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace StudentRegistrationSystem.API.Controllers;

[Route("api/topics")]
[ApiController]
public class TopicsController : ControllerBase
{
    private readonly ITopicService _service;
    private readonly IValidator<CreateTopicDTO> _createValidator;
    private readonly IValidator<UpdateTopicDTO> _updateValidator;
    private readonly ILogger<TopicsController> _logger;
    public TopicsController(ITopicService service,
        IValidator<CreateTopicDTO> createValidator,
        IValidator<UpdateTopicDTO> updateValidator,
        ILogger<TopicsController> logger)
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
    public async Task<IActionResult> CreateAsync([FromBody] CreateTopicDTO dto)
    {
        var validationResult = await _createValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning($"Topic: '{dto.Name}' failed validation.");

            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        try
        {
            await _service.CreateAsync(dto);

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
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateTopicDTO dto)
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
