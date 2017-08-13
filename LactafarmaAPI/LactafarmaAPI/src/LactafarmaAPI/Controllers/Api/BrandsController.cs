using System;
using LactafarmaAPI.Controllers.Api.Base;
using LactafarmaAPI.Controllers.Api.Interfaces;
using LactafarmaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LactafarmaAPI.Controllers.Api
{
    public class BrandsController : BaseController, IBrandsController
    {
        #region Private Properties

        private readonly ILogger<BrandsController> _logger;

        #endregion

        #region Constructors

        public BrandsController(ILactafarmaService lactafarmaService, IMailService mailService,
            IConfigurationRoot config,
            ILogger<BrandsController> logger, IMemoryCache cache) : base(lactafarmaService, mailService, config, cache)
        {
            _logger = logger;

            CacheInitialize(lactafarmaService.GetAllBrands(), EntityType.Brand);
        }

        #endregion

        #region Public Methods

        public JsonResult GetBrandsByDrug(int drugId)
        {
            var result = new JsonResult(string.Empty);
            try
            {
                _logger.LogInformation("BEGIN GetBrandsByDrug");
                result = Json(LactafarmaService.GetBrandsByDrug(drugId));
                _logger.LogInformation("END GetBrandsByDrug");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetBrandsByDrug(drugId={drugId}) with message {ex.Message}");
            }

            return result;
        }

        public JsonResult GetBrand(int brandId)
        {
            var result = new JsonResult(string.Empty);
            try
            {
                _logger.LogInformation("BEGIN GetBrand");
                result = Json(LactafarmaService.GetBrand(brandId));
                _logger.LogInformation("END GetBrand");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetBrand(brandId={brandId}) with message {ex.Message}");
            }

            return result;
        }

        #endregion
    }
}