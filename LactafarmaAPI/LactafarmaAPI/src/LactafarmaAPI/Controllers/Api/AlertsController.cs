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
    /// <summary>
    /// Alerts Controller
    /// </summary>
    public class AlertsController : BaseController, IAlertsController
    {
        #region Private Properties

        private readonly ILogger<AlertsController> _logger;

        #endregion

        #region Constructors
        /// <summary>
        /// Alerts handler constructor
        /// </summary>
        /// <param name="lactafarmaService"></param>
        /// <param name="mailService"></param>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        /// <param name="cache"></param>
        /// <param name="userManager"></param>
        public AlertsController(ILactafarmaService lactafarmaService, IMailService mailService,
            IConfigurationRoot config,
            ILogger<AlertsController> logger, IMemoryCache cache, UserManager<User> userManager) : base(lactafarmaService, mailService, config, cache, userManager)
        {
            _logger = logger;

            //CacheInitialize(lactafarmaService.GetAllAlerts());
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get alerts by specified Drug in User context
        /// </summary>
        /// <param name="drugId"></param>
        /// <returns></returns>
        [HttpGet("bydrug/{drugId:int}")]
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