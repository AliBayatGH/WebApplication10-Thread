using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication10.Controllers;
[Route("api/test")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly ILogger<ValuesController> _logger;

    public ValuesController(ILogger<ValuesController> logger)
    {
        this._logger = logger;
    }

    [HttpGet("thread")]
    public IActionResult SimulateBlocking()
    {
        _logger.LogInformation("Thread.Sleep simulation started at {Time}", DateTime.Now);
        Thread.Sleep(10000); // Block the thread for 10 seconds
        _logger.LogInformation("Thread.Sleep simulation ended at {Time}", DateTime.Now);
        return Ok("Thread.Sleep simulation completed.");
    }

    [HttpGet("task")]
    public async Task<IActionResult> SimulateNonBlocking()
    {
        _logger.LogInformation("Task.Delay simulation started at {Time}", DateTime.Now);
        await Task.Delay(10000); // Wait asynchronously for 10 seconds
        _logger.LogInformation("Task.Delay simulation ended at {Time}", DateTime.Now);
        return Ok("Task.Delay simulation completed.");
    }

}
