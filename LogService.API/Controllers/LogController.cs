using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LogService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;
        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult ReceiveLog([FromBody] LogEntryDto log)
        {
            // Structured log örneği
            _logger.Log(log.Level, "{Service} - {Message} - {Details}", log.Service, log.Message, log.Details);
            return Ok();
        }
    }

    public class LogEntryDto
    {
        public string Service { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public LogLevel Level { get; set; } = LogLevel.Information;
    }
}