using System;
using LactafarmaAPI.Controllers.Api.Base;
using LactafarmaAPI.Controllers.Api.Interfaces;
using LactafarmaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
            ILogger<DrugsController> logger) : base(lactafarmaService, mailService, config)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods
        [Route("bygroup/{groupId:int}")]
        public JsonResult GetDrugsByGroup(int groupId)
        {
            var result = new JsonResult(string.Empty);
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

            return result;
        }

        public JsonResult GetDrugsByBrand(int brandId)
        {
            var result = new JsonResult(string.Empty);
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

            return result;
        }

        public JsonResult GetDrugByAlias(int aliasId)
        {
            var result = new JsonResult(string.Empty);
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

            return result;
        }

        public JsonResult GetDrug(int drugId)
        {
            var result = new JsonResult(string.Empty);
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

            return result;
        }

        #endregion
    }
}