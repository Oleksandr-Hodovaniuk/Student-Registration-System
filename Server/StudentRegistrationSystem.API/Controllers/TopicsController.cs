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
}
