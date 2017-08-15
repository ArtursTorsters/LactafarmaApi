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
    public class DrugsController : BaseController, IDrugsController
    {
        #region Private Properties

        private readonly ILogger<DrugsController> _logger;

        #endregion

        #region Constructors

        public DrugsController(ILactafarmaService lactafarmaService, IMailService mailService,
            IConfigurationRoot config,
            ILogger<DrugsController> logger, IMemoryCache cache) : base(lactafarmaService, mailService, config, cache)
        {
            _logger = logger;

            CacheInitialize(lactafarmaService.GetAllDrugs(), EntityType.Drug);
        }

        #endregion

        #region Public Methods

        [Route("byname/{startsWith}")]
        public JsonResult GetDrugsByName(string startsWith)
        {
            JsonResult result = null;
            try
            {
                _logger.LogInformation("BEGIN GetDrugsByName");

                if (startsWith.Length < 1)
                {
                    _logger.LogWarning("Call to the API without any letter!!");
                    return null;
                }

                Cache.TryGetValue(EntityType.Drug, out IEnumerable<BaseModel> drugs);

                result = Json(drugs
                    .Where(a => a.VirtualName.IndexOf(startsWith.RemoveDiacritics(), StringComparison.CurrentCultureIgnoreCase) !=
                                -1).Take(7));

                _logger.LogInformation("END GetDrugsByName");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetDrugsByName(name={startsWith}) with message {ex.Message}");
            }
            finally
            {
                if (result?.Value == null)
                    _logger.LogWarning("No results for current request!!!");
            }
            return result;
        }

        [Route("bygroup/{groupId:int}")]
        public JsonResult GetDrugsByGroup(int groupId)
        {
            JsonResult result = null;
            try
            {
                _logger.LogInformation("BEGIN GetDrugsByGroup");
                result = Json(LactafarmaService.GetDrugsByGroup(groupId));
                _logger.LogInformation("END GetDrugsByGroup");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetDrugsByGroup(groupId={groupId}) with message {ex.Message}");
            }
            finally
            {
                if (result?.Value == null)
                    _logger.LogWarning("No results for current request!!!");
            }
            return result;
        }

        [Route("bybrand/{brandId:int}")]
        public JsonResult GetDrugsByBrand(int brandId)
        {
            JsonResult result = null;
            try
            {
                _logger.LogInformation("BEGIN GetDrugsByBrand");
                result = Json(LactafarmaService.GetDrugsByBrand(brandId));
                _logger.LogInformation("END GetDrugsByBrand");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetDrugsByBrand(brandId={brandId}) with message {ex.Message}");
            }
            finally
            {
                if (result?.Value == null)
                    _logger.LogWarning("No results for current request!!!");
            }
            return result;
        }

        [Route("byalias/{aliasId:int}")]
        public JsonResult GetDrugByAlias(int aliasId)
        {
            JsonResult result = null;
            try
            {
                _logger.LogInformation("BEGIN GetDrugByAlias");
                result = Json(LactafarmaService.GetDrugByAlias(aliasId));
                _logger.LogInformation("END GetDrugByAlias");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetDrugByAlias(aliasId={aliasId}) with message {ex.Message}");
            }
            finally
            {
                if (result?.Value == null)
                    _logger.LogWarning("No results for current request!!!");
            }
            return result;
        }

        [Route("{drugId:int}")]
        public JsonResult GetDrug(int drugId)
        {
            JsonResult result = null;
            try
            {
                _logger.LogInformation("BEGIN GetDrug");
                result = Json(LactafarmaService.GetDrug(drugId));
                _logger.LogInformation("END GetDrug");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on JsonResult called GetDrug(drugId={drugId}) with message {ex.Message}");
            }
            finally
            {
                if (result?.Value == null)
                    _logger.LogWarning("No results for current request!!!");
            }
            return result;
        }

        #endregion
    }
}