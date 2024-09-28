using Microsoft.AspNetCore.Mvc;
using TaxAssistant.External.Services;

namespace TaxAssistant.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly ILLMService lmmService;

    public TestController(ILLMService lmmService)
    {
        this.lmmService = lmmService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await lmmService.GenerateMessageAsync("Hello", "text"));
    }
}
