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
    public class AlertsController : BaseController, IAlertsController
    {
        #region Private Properties

        private readonly ILogger<AlertsController> _logger;

        #endregion

        #region Constructors

        public AlertsController(ILactafarmaService lactafarmaService, IMailService mailService,
            IConfigurationRoot config,
            ILogger<AlertsController> logger, IMemoryCache cache) : base(lactafarmaService, mailService, config, cache)
        {
            _logger = logger;

            //CacheInitialize(lactafarmaService.GetAllAlerts());
        }

        #endregion

        #region Public Methods

        public JsonResult GetAlertsByDrug(int drugId)
        {
            var result = new JsonResult(string.Empty);
            try
            {
                _logger.LogInformation("BEGIN GetAlertsByDrug");
                result = Json(LactafarmaService.GetAlertsByDrug(drugId));
                _logger.LogInformation("END GetAlertsByDrug");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetAlertsByDrug(drugId={drugId}) with message {ex.Message}");
            }

            return result;
        }

        #endregion
    }
}