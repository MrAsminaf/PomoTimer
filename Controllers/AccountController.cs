﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PomoTimer.Data;
using PomoTimer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PomoTimer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITimeModelRepository timerModelRepository;
        private readonly ILogger<AccountController> logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITimeModelRepository timerModelRepository,
            ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.timerModelRepository = timerModelRepository;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToRoute("Home");
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, 
                    model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                    return View();
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails()
        {
            var user = await userManager.GetUserAsync(User);

            UserDetailsViewModel viewModel = new UserDetailsViewModel();
            viewModel.TimeModelsLastWeekGrouped = timerModelRepository.GetUserTimeModelsInLastWeekGrouped(user.Id);;
            viewModel.TimeModelsLastWeekEmpty = timerModelRepository.GetUserTimeModelsInLastWeek(user.Id)
                .Where(x => x.TaskName == "")
                .OrderBy(x => x.DateTime);

            return View(viewModel);
        }

        [HttpPatch]
        public async Task<IActionResult> AddTask()
        {
            logger.LogInformation("Added task");
            return Ok();
        }
    }
}
