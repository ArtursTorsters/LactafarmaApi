using System;
using LactafarmaAPI.Controllers.Api.Base;
using LactafarmaAPI.Controllers.Api.Interfaces;
using LactafarmaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LactafarmaAPI.Controllers.Api
{
    public class AliasesController : BaseController, IAliasesController
    {
        #region Private Properties

        private readonly ILogger<AliasesController> _logger;

        #endregion

        #region Constructors

        public AliasesController(ILactafarmaService lactafarmaService, IMailService mailService,
            IConfigurationRoot config,
            ILogger<AliasesController> logger) : base(lactafarmaService, mailService, config)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public JsonResult GetAliasesByDrug(int drugId)
        {
            var result = new JsonResult(string.Empty);
            try
            {
                _logger.LogInformation("BEGIN GetAliasesByDrug");
                result = Json(LactafarmaService.GetAliasesByDrug(drugId));
                _logger.LogInformation("END GetAliasesByDrug");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetAliasesByDrug(drugId={drugId}) with message {ex.Message}");
            }

            return result;
        }

        public JsonResult GetAlias(int aliasId)
        {
            var result = new JsonResult(string.Empty);
            try
            {
                _logger.LogInformation("BEGIN GetAlias");
                result = Json(LactafarmaService.GetAlias(aliasId));
                _logger.LogInformation("END GetAlias");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetAlias(aliasId={aliasId}) with message {ex.Message}");
            }

            return result;
        }

        #endregion
    }
}