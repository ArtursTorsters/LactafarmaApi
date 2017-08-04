using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LactafarmaAPI.Data;
using LactafarmaAPI.Services;
using LactafarmaAPI.ViewModels.Demo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LactafarmaAPI.Controllers
{
    public class DemoController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private LactafarmaContext _context;

        public DemoController(IMailService mailService, IConfigurationRoot config, LactafarmaContext context)
        {
            _mailService = mailService;
            _config = config;
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var drugAlternativesCount = _context.DrugAlternatives.Count();
            var drugAlternatives = _context.DrugAlternatives.Take(10);

            var drugAlternatives2 = _context.DrugAlternatives.Include(e => e.Drug).Include(e => e.Drug.DrugsMultilingual).Where(e => e.DrugId == 320);

            var brandsCount = _context.Brands.Count();
            var brands= _context.Brands.Take(10);

            var brandsMultilingualCount = _context.BrandsMultilingual.Count();
            var brandsMultilingual = _context.BrandsMultilingual.Include(e => e.Brand).Include(e => e.Language);
            var brandsMultilingual2 = _context.BrandsMultilingual.Include(e => e.Brand).Include(e => e.Language).Where(e => e.BrandId == 14);

            var groupsCount = _context.Groups.Count();
            var groups = _context.Groups.Take(10);

            var groupsMultilingualCount = _context.GroupsMultilingual.Count();
            var groupsMultilingual = _context.GroupsMultilingual.Take(10);

            var languagesCount = _context.Languages.Count();
            var languages = _context.Languages.Take(2);

            var usersCount = _context.Users.Count();
            var users = _context.Users.Take(2);

            var tokensCount = _context.Tokens.Count();
            var tokens = _context.Tokens.Take(2);

            var favoritesCount = _context.Favorites.Count();
            var favorites = _context.Favorites.Take(2);

            var drugsCount = _context.Drugs.Count();
            var drugs = _context.Drugs.Take(10);

            var drugsMultilingualCount = _context.DrugsMultilingual.Count();
            var drugsMultilingual = _context.DrugsMultilingual.Take(10);

            var alertsCount = _context.Alerts.Count();
            var alerts = _context.Alerts.Take(10);

            var alertsMultilingualCount = _context.AlertsMultilingual.Count();
            var alertsMultilingual = _context.AlertsMultilingual.Take(10);


            var aliasesCount = _context.Aliases.Count();
            var aliases = _context.Aliases.Take(10);

            var aliasesMultilingualCount = _context.AliasMultilingual.Count();
            var aliasesMultilingual = _context.AliasMultilingual.Take(10);









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
