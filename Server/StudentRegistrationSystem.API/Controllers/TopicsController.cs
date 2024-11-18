using Application.DTOs;
using Application.Exceptions;
using Application.Services.Interfaces;
using Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace StudentRegistrationSystem.API.Controllers;

[Route("api/topics")]
[ApiController]
public class TopicsController(
    ITopicService service,
    IValidator<CreateTopicDTO> createValidator,
    IValidator<UpdateTopicDTO> updateValidator,
    ILogger<TopicsController> logger) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var topics = await service.GetAllAsync();

            if (!topics.Any())
            {
                return NoContent();
            }

            return Ok(topics);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while getting topic.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTopicDTO dto)
    {
        var validationResult = await createValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            logger.LogWarning($"Topic: '{dto.Name}' failed validation.");

            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        try
        {
            await service.CreateAsync(dto);

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
            logger.LogError(ex, "An unexpected error occurred while creating topic.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateTopicDTO dto)
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
        catch(NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while updating topic.");

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
            logger.LogError(ex, "An unexpected error occurred while deleting topic.");

            return StatusCode(500, "Internal server error.");
        }
    }
}
