using System;
using System.Threading.Tasks;
using LactafarmaAPI.Controllers.Api.Base;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Data.PagedData;
using LactafarmaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LactafarmaAPI.Controllers.Api
{
    public class LogController : BaseController
    {
        #region Private Properties

        private readonly ILogger<LogController> _logger;

        #endregion

        #region Constructors

        public LogController(ILactafarmaService lactafarmaService, IMailService mailService,
            IConfigurationRoot config, ILogger<LogController> logger, IMemoryCache cache, UserManager<User> userManager) : base(lactafarmaService, mailService, config, cache, userManager)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods

        [Route("levels")]
        public async Task<IActionResult> GetLevels()
        {
            var result = new JsonResult(false);
            try
            {
                _logger.LogInformation("BEGIN GetLevels");
                var availableLevels = await LactafarmaService.GetLevelsAsync();
                result = Json(availableLevels);
                _logger.LogInformation("END GetLevels");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetLevels with message {ex.Message}");
            }
            finally
            {
                if (result == null)
                    _logger.LogWarning("No results for current request!!!");
            }

            return result;
        }

        /// <summary>
        ///     Get logs with Error level specified on last 7 days
        /// </summary>
        /// <returns></returns>
        [Route("error")]
        public async Task<IActionResult> GetErrorLogs()
        {
            IActionResult result = null;
            try
            {
                _logger.LogInformation("BEGIN GetErrorLogs");
                var logs = await LactafarmaService.GetLogsAsync(new LogPagedDataRequest
                {
                    FromDate = DateTime.Now.Subtract(TimeSpan.FromDays(7)),
                    ToDate = DateTime.Now,
                    Level = "Error",
                    PageSize = 100
                });
                result = Json(logs);

                _logger.LogInformation("END GetErrorLogs");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetErrorLogs with message {ex.Message}");
            }
            finally
            {
                if (result == null)
                    _logger.LogWarning("No results for current request!!!");
            }

            return result;
        }

        /// <summary>
        ///     Get logs with Error level specified on last 7 days
        /// </summary>
        /// <returns></returns>
        [Route("warn")]
        public async Task<IActionResult> GetWarnLogs()
        {
            IActionResult result = null;
            try
            {
                _logger.LogInformation("BEGIN GetErrorLogs");
                var logs = await LactafarmaService.GetLogsAsync(new LogPagedDataRequest
                {
                    FromDate = DateTime.Now.Subtract(TimeSpan.FromDays(7)),
                    ToDate = DateTime.Now,
                    Level = "Warn",
                    PageSize = 100
                });
                result = Json(logs);

                _logger.LogInformation("END GetErrorLogs");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetErrorLogs with message {ex.Message}");
            }
            finally
            {
                if (result == null)
                    _logger.LogWarning("No results for current request!!!");
            }

            return result;
        }

        [Route("fatal")]
        public async Task<IActionResult> GetFatalLogs()
        {
            IActionResult result = null;
            try
            {
                _logger.LogInformation("BEGIN GetFatalLogs");
                var logs = await LactafarmaService.GetLogsAsync(new LogPagedDataRequest
                {
                    FromDate = DateTime.Now.Subtract(TimeSpan.FromDays(7)),
                    ToDate = DateTime.Now,
                    Level = "Fatal",
                    PageSize = 100
                });
                result = Json(logs);

                _logger.LogInformation("END GetFatalLogs");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetFatalLogs with message {ex.Message}");
            }
            finally
            {
                if (result == null)
                    _logger.LogWarning("No results for current request!!!");
            }

            return result;
        }

        /// <summary>
        ///     Get logs with Any level specified on last 7 days
        /// </summary>
        /// <returns></returns>
        [Route("all")]
        public async Task<IActionResult> GetLogs()
        {
            IActionResult result = null;
            try
            {
                _logger.LogInformation("BEGIN GetLogs");
                var logs = await LactafarmaService.GetLogsAsync(new LogPagedDataRequest
                {
                    FromDate = DateTime.Now.Subtract(TimeSpan.FromDays(7)),
                    ToDate = DateTime.Now,
                    PageSize = 500
                });
                result = Json(logs);
                _logger.LogInformation("END GetLogs");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetLogs with message {ex.Message}");
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