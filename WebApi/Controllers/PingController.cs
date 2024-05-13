using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("ping")]
public class PingController : ControllerBase
{
    private readonly ILogger<PingController> _logger;

    public PingController(ILogger<PingController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation(".....................Get was called .........................");
        return Ok();
    }
}