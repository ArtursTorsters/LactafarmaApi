using System;
using System.Collections.Generic;
using System.Linq;
using LactafarmaAPI.Controllers.Api.Base;
using LactafarmaAPI.Controllers.Api.Interfaces;
using LactafarmaAPI.Domain.Models.Base;
using LactafarmaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
            ILogger<AliasesController> logger, IMemoryCache cache) : base(lactafarmaService, mailService, config, cache)
        {
            _logger = logger;

            CacheInitialize(lactafarmaService.GetAllAliases(), EntityType.Alias);
        }

        #endregion

        #region Public Methods

        [Route("byname/{startsWith:string}")]
        public JsonResult GetAliasesByName(string startsWith)
        {
            var result = new JsonResult("");
            if (startsWith.Length < 1) return result;

            Cache.TryGetValue(EntityType.Alias, out IEnumerable<BaseModel> aliases);

            result = Json(aliases
                .Where(a => a.VirtualName.IndexOf(startsWith, StringComparison.CurrentCultureIgnoreCase) !=
                            -1).Take(3));

            return result;
        }

        [Route("bydrug/{drugId:int}")]
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

        [Route("{aliasId:int}")]
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