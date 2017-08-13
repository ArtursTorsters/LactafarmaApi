using System;
using System.Collections.Generic;
using System.Linq;
using LactafarmaAPI.Controllers.Api.Base;
using LactafarmaAPI.Controllers.Api.Interfaces;
using LactafarmaAPI.Core;
using LactafarmaAPI.Domain.Models.Base;
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

        [Route("byname/{startsWith}")]
        public JsonResult GetBrandsByName(string startsWith)
        {
            JsonResult result = null;
            if (startsWith.Length < 1) return result;

            Cache.TryGetValue(EntityType.Brand, out IEnumerable<BaseModel> brands);

            result = Json(brands
                .Where(a => a.VirtualName.IndexOf(startsWith.RemoveDiacritics(), StringComparison.CurrentCultureIgnoreCase) !=
                            -1).Take(3));

            return result;
        }

        [Route("bydrug/{drugId:int}")]
        public JsonResult GetBrandsByDrug(int drugId)
        {
            JsonResult result = null;
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

        [Route("{brandId:int}")]
        public JsonResult GetBrand(int brandId)
        {
            JsonResult result = null;
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