using System;
using LactafarmaAPI.Services.Interfaces;
using LactafarmaAPI.ViewModels.Demo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LactafarmaAPI.Controllers
{
    public class DemoController : Controller
    {
        #region Private Properties

        private readonly IConfigurationRoot _config;
        private readonly ILactafarmaService _lactafarmaService;
        private readonly IMailService _mailService;
        private readonly ILogger<DemoController> _logger;

        #endregion

        #region Constructors

        public DemoController(ILactafarmaService lactafarmaService, IMailService mailService, IConfigurationRoot config,
            ILogger<DemoController> logger)
        {
            _lactafarmaService = lactafarmaService;
            _mailService = mailService;
            _config = config;
            _logger = logger;
        }

        #endregion

        #region Public Methods

        // GET: /<controller>/
        public IActionResult Index()
        {
            try
            {
                var aliases = _lactafarmaService.GetAliasesByDrug(28);
                var brands = _lactafarmaService.GetBrandsByDrug(1);
                var drugs = _lactafarmaService.GetDrugsByGroup(5);
                var drugBrands = _lactafarmaService.GetDrugsByBrand(2);
                var group = _lactafarmaService.GetGroup(105);
                var user = _lactafarmaService.GetUser("C9B2B0B0-9A73-4671-8BA7-0DBE5F68D936");
                var alias = _lactafarmaService.GetAlias(12);
                var drugAlias = _lactafarmaService.GetDrugByAlias(12);
                var brand = _lactafarmaService.GetBrand(8);
                var drug = _lactafarmaService.GetDrug(428);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Error with service connection trying to query something with following message: {ex.Message}");

                return Redirect("/error");
            }

            return View();
        }

        public IActionResult Demo()
        {
            throw new InvalidOperationException("Invalid operation happens because your are working right now...");
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
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "From Me", model.Message);

            return View();
        }

        #endregion

        //    var brandsMultilingualCount = _context.BrandsMultilingual.Count();
        //    var brands = _context.Brands.Take(10);

        //    var brandsCount = _context.Brands.Count();
        //        .Include(e => e.Drug.DrugsMultilingual).Where(e => e.DrugId == 320);

        //    var drugAlternatives2 = _context.DrugAlternatives.Include(e => e.Drug)
        //    var drugAlternatives = _context.DrugAlternatives.Take(10);
        //    var drugAlternativesCount = _context.DrugAlternatives.Count();
        //{

        //private void TestCalls()
        //    var brandsMultilingual = _context.BrandsMultilingual.Include(e => e.Brand).Include(e => e.Language);
        //    var brandsMultilingual2 = _context.BrandsMultilingual.Include(e => e.Brand).Include(e => e.Language)
        //        .Where(e => e.BrandId == 14);

        //    var groupsCount = _context.Groups.Count();
        //    var groups = _context.Groups.Take(10);

        //    var groupsMultilingualCount = _context.GroupsMultilingual.Count();
        //    var groupsMultilingual = _context.GroupsMultilingual.Take(10);

        //    var languagesCount = _context.Languages.Count();
        //    var languages = _context.Languages.Take(2);

        //    var usersCount = _context.Users.Count();
        //    var users = _context.Users.Take(2);

        //    var tokensCount = _context.Tokens.Count();
        //    var tokens = _context.Tokens.Take(2);

        //    var favoritesCount = _context.Favorites.Count();
        //    var favorites = _context.Favorites.Take(2);

        //    var drugsCount = _context.Drugs.Count();
        //    var drugs = _context.Drugs.Take(10);

        //    var drugsMultilingualCount = _context.DrugsMultilingual.Count();
        //    var drugsMultilingual = _context.DrugsMultilingual.Take(10);

        //    var alertsCount = _context.Alerts.Count();
        //    var alerts = _context.Alerts.Take(10);

        //    var alertsMultilingualCount = _context.AlertsMultilingual.Count();
        //    var alertsMultilingual = _context.AlertsMultilingual.Take(10);


        //    var aliasesCount = _context.Aliases.Count();
        //    var aliases = _context.Aliases.Take(10);

        //    var aliasesMultilingualCount = _context.AliasMultilingual.Count();
        //    var aliasesMultilingual = _context.AliasMultilingual.Take(10);
        //}
    }
}