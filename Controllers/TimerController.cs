using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PomoTimer.Models;

namespace PomoTimer.Controllers
{
    [EnableCors("localhostPolicy")]
    [Controller]
    public class TimerController : Controller
    {
        private readonly ILogger<TimerController> logger;

        public TimerController(ILogger<TimerController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult updateStats([FromBody] UpdateStatsModel model)
        {
            if (model == null)
            {
                logger.LogInformation("Model is null");
                return BadRequest();
            }
            logger.LogInformation($"Updated data by {model.Hours} hours and " +
                $"{model.Minutes} minutes");

            return Ok();
        }
    }
}
