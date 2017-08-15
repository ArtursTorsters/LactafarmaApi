using System;
using System.Threading.Tasks;
using LactafarmaAPI.Controllers.Api.Base;
using LactafarmaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LactafarmaAPI.Controllers.Api
{
    public class CacheController : BaseController
    {
        #region Private Properties

        private readonly ILogger<CacheController> _logger;

        #endregion

        #region Constructors

        public CacheController(ILactafarmaService lactafarmaService, IMailService mailService,
            IConfigurationRoot config, ILogger<CacheController> logger, IMemoryCache cache) : base(lactafarmaService,
            mailService, config, cache)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods

        [Route("load")]
        public async Task<IActionResult> LoadCaches()
        {
            JsonResult result = null;
            try
            {
                _logger.LogInformation("BEGIN LoadCaches");
                await LoadCachesAsync();
                result = new JsonResult(true);
                _logger.LogInformation("END LoadCaches");
            }
            catch (Exception ex)
            {
                result = new JsonResult(false);
                _logger.LogError(
                    $"Exception on JsonResult called LoadCaches with message {ex.Message}");
            }
            finally
            {
                if (result == Json(false))
                    _logger.LogWarning("No results for current request!!!");
            }

            return result;
        }

        [Route("clear")]
        public async Task<IActionResult> ClearCaches()
        {
            var result = new JsonResult(false);
            try
            {
                _logger.LogInformation("BEGIN ClearCaches");
                await ClearCachesAsync();
                result = new JsonResult(true);
                _logger.LogInformation("END ClearCaches");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called ClearCaches with message {ex.Message}");
            }

            return result;
        }

        #endregion

        #region Private Methods

        private async Task LoadCachesAsync()
        {
            try
            {
                _logger.LogInformation("BEGIN LoadCachesAsync");
                await Task.Run(() => CacheInitialize(LactafarmaService.GetAllDrugs(), EntityType.Drug));
                await Task.Run(() => CacheInitialize(LactafarmaService.GetAllAliases(), EntityType.Alias));
                await Task.Run(() => CacheInitialize(LactafarmaService.GetAllBrands(), EntityType.Brand));
                await Task.Run(() => CacheInitialize(LactafarmaService.GetAllGroups(), EntityType.Group));
                _logger.LogInformation("END LoadCachesAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called LoadCachesAsync with message {ex.Message}");
            }
        }

        #endregion
    }
}