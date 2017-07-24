using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LactafarmaAPI.Services;
using LactafarmaAPI.ViewModels.Demo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LactafarmaAPI.Controllers
{
    public class DemoController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;

        public DemoController(IMailService mailService, IConfigurationRoot config)
        {
            _mailService = mailService;
            _config = config;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Demo()
        {
            throw new InvalidOperationException("Invalid operation happens because your are working right now...");

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("ocu.org")) ModelState.AddModelError("Email", "We don't support OCU addresses");
            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "From Me", model.Message);
            }

            return View();
        }
    }
}
