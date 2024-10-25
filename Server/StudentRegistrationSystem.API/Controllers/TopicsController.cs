using Application.DTOs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace StudentRegistrationSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopicsController : ControllerBase
{
    private readonly ITopicService _service;
    public TopicsController(ITopicService service)
    {
        _service = service;
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
        if (!ModelState.IsValid || topic == null || topic.Id != 0)
        {
            return BadRequest(ModelState);
        }

        await _service.CreateAsync(topic);

        return Created();
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
