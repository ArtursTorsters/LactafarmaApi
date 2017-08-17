using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LactafarmaAPI.Controllers.Api;
using LactafarmaAPI.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LactafarmaAPI.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace LactafarmaAPI.Controllers.Web
{
    public class AuthController : Controller
    {
        private SignInManager<User> _signInManager;
        private ILogger<AuthController> _logger;
        private IMemoryCache _cache;
        private UserManager<User> _userManager;

        public AuthController(SignInManager<User> signInManager, ILogger<AuthController> logger, IMemoryCache cache, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _cache = cache;
            _userManager = userManager;
        }

        [Route("/auth/login")]
        public ActionResult Login()
        {
            ActionResult result = null;
            try
            {
                _logger.LogInformation("BEGIN Login");
                if (!User.Identity.IsAuthenticated)
                {
                    return View();
                }
                
                _logger.LogInformation("END Login");
                result = Json("User authenticated");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called Login with message {ex.Message}");
            }

            return View();
        }

        [Route("/auth/login")]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            ActionResult result = null;
            try
            {
                _logger.LogInformation("BEGIN POST Login");
                if (ModelState.IsValid)
                {
                    var signinResult = await _signInManager.PasswordSignInAsync(model.UserName,
                        model.Password,
                        false, false);
                    if (signinResult.Succeeded)
                    {
                        _logger.LogInformation("User authenticated succesfully");
                        return Json("User authenticated succesfully");
                    }
                }

                // Just say Login failed on all errors
                ModelState.AddModelError("", "Login Failed");
                _logger.LogWarning("Login Failed");

                _logger.LogInformation("END POST Login");
                result = Json("Credentials not valid");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called POST Login with message {ex.Message}");
            }

            return result;
        }
        [Route("/auth/logout")]
        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            _cache.Remove("User");
            _logger.LogInformation("User did logout correctly!!");

            return Json("User did logout correctly!!");
        }

    }
}
