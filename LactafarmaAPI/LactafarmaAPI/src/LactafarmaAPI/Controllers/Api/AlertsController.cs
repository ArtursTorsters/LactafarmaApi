using System;
using LactafarmaAPI.Controllers.Api.Base;
using LactafarmaAPI.Controllers.Api.Interfaces;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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
            ILogger<AlertsController> logger, IMemoryCache cache, UserManager<User> userManager) : base(lactafarmaService, mailService, config, cache, userManager)
        {
            _logger = logger;

            //CacheInitialize(lactafarmaService.GetAllAlerts());
        }

        #endregion

        #region Public Methods

        [Route("bydrug/{drugId:int}")]
        public IEnumerable<Domain.Models.Alert> GetAlertsByDrug(int drugId)
        {
            IEnumerable<Domain.Models.Alert> result = null;
            try
            {
                _logger.LogInformation("BEGIN GetAlertsByDrug");
                result = LactafarmaService.GetAlertsByDrug(drugId);
                _logger.LogInformation("END GetAlertsByDrug");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetAlertsByDrug(drugId={drugId}) with message {ex.Message}");
            }
            finally
            {
                if (result == null)
                    _logger.LogWarning("No results for current request!!!");
            }
            return result;
        }

        #endregion
    }
}