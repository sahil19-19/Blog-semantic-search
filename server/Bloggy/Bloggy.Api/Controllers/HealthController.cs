using Microsoft.AspNetCore.Mvc;

namespace Bloggy.Api.Controllers;

public class HealthController : BloggyControllerBase
{
    [HttpGet("/health")]
    public IActionResult Health()
    {
        return Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow });
    }
}
