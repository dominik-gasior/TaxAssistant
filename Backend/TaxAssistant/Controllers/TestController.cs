using Microsoft.AspNetCore.Mvc;

namespace TaxAssistant.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Test");
    }
}
