using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Language = LactafarmaAPI.ViewModels.Language;

namespace LactafarmaAPI.Controllers.Web
{
    public class AccountController: Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        //private readonly IEmailSender _emailSender;
        //private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        private readonly IMemoryCache _cache;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            //IEmailSender emailSender,
            //ISmsSender smsSender,
            ILoggerFactory loggerFactory, IMemoryCache cache)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_emailSender = emailSender;
            //_smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _cache = cache;
        }

        // GET: /Account/Register
        [Route("/account/register")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var vm = new RegisterViewModel
            {
                Languages = new List<Language>
                {
                    new Language
                    {
                        Id = Guid.Parse("7C0AFE0E-0B25-4AEA-8AAE-51CBDDE1B134"),
                        Name = "Spanish"
                    },
                    new Language
                    {
                        Id = Guid.Parse("458AD052-C8AC-486B-A945-FB3A85219448"),
                        Name = "English"
                    }
                }
            };
            return View(vm);
        }

        // POST: /Account/Register
        [Route("/account/register")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email, LanguageId = model.LanguageId };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User created a new account with password.");
                    return Json("User created succesfully");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
