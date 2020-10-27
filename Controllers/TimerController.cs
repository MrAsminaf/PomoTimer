using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PomoTimer.Models;
using System;
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

        public TimerController(ILogger<TimerController> logger, UserManager<ApplicationUser> userManager)
        {
            this.logger = logger;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> updateStats()
        {
            ClaimsPrincipal currentUser = this.User;

            if (currentUser != null)
            {
                var user = await userManager.GetUserAsync(currentUser);
                //user.TimeSpan = user.TimeSpan.Add(new TimeSpan(0, 1, 0));
                //await userManager.UpdateAsync(user);
                //logger.LogInformation($"Updated {user.Email}'s data to {user.TimeSpan}");


                // TO FIX: https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities
                if (user == null)
                {
                    logger.LogInformation("Could not find user");
                    return BadRequest();
                }

                var minutesInCurrentDay = user.Minutes.FindAll(date => date.DateTime == DateTime.Now);

                if (minutesInCurrentDay == null)
                {
                    logger.LogInformation("No data from today");
                }
                else
                {
                    logger.LogInformation("Some data from today exists");
                }

                return Ok();
            }
            return BadRequest();
        }
    }
}
