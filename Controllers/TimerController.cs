using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.Logging;
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

                if (user == null)
                {
                    logger.LogInformation("Could not find user");
                    return BadRequest();
                }

                return Ok();
            }
            return BadRequest();
        }
    }
}
