using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LactafarmaAPI.Services;
using LactafarmaAPI.ViewModels.Demo;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LactafarmaAPI.Controllers
{
    public class DemoController : Controller
    {
        private IMailService _mailService;

        public DemoController(IMailService mailService)
        {
            _mailService = mailService;
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
            _mailService.SendMail("dgomez@ocu.org", model.Email, "From Me", model.Message);

            return View();
        }
    }
}
