using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LactafarmaAPI.Controllers.Api
{
    public class ErrorController : Controller
    {
        #region Private Properties

        private readonly ILogger<ErrorController> _logger;

        #endregion

        #region Constructors

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods

        [Route("{*url}", Order = 999)]
        public JsonResult TraceError()
        {
            JsonResult result = null;
            try
            {
                _logger.LogCritical("Page Not Found");
                result = Json("404: Page Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called TraceError with message {ex.Message}");
            }

            return result;
        }

        #endregion
    }
}