using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.Logging;
using PomoTimer.Data;
using PomoTimer.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PomoTimer.Controllers
{
    [EnableCors("localhostPolicy")]
    [Controller]
    public class TimerController : Controller
    {
        private readonly ILogger<TimerController> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITimeModelRepository timeModelRepository;

        public TimerController(
            ILogger<TimerController> logger, 
            UserManager<ApplicationUser> userManager,
            ITimeModelRepository timeModelRepository)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.timeModelRepository = timeModelRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStats([FromBody]UpdateStatsModel model)
        {
            ClaimsPrincipal currentUser = this.User;

            //logger.LogInformation($"Model minutes: {model.Minutes}");
            //logger.LogInformation($"Model Task Name: {model.TaskName}");

            if (currentUser != null)
            {
                var user = await userManager.GetUserAsync(currentUser);

                if (user == null)
                {
                    logger.LogInformation("Could not find user");
                    return BadRequest();
                }

                timeModelRepository.AddTimeToUser(user.Id, DateTime.Now, model);
                timeModelRepository.Save();

                return Ok();
            }
            return BadRequest();
        }
    }
}
