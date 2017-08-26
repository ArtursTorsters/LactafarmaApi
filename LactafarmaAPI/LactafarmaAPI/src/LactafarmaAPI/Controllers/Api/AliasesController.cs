using System;
using System.Collections.Generic;
using System.Linq;
using LactafarmaAPI.Controllers.Api.Base;
using LactafarmaAPI.Controllers.Api.Interfaces;
using LactafarmaAPI.Core;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Domain.Models.Base;
using LactafarmaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
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
            ILogger<AliasesController> logger, IMemoryCache cache, UserManager<User> userManager) : base(lactafarmaService, mailService, config, cache, userManager)
        {
            _logger = logger;

            CacheInitialize(lactafarmaService.GetAllAliases(), EntityType.Alias);
        }

        #endregion

        #region Public Methods

        [Route("byname/{startsWith}")]
        public IEnumerable<Domain.Models.Alias> GetAliasesByName(string startsWith)
        {
            IEnumerable<Domain.Models.Alias> result = null;
            try
            {
                _logger.LogInformation("BEGIN GetAliasesByName");

                if (startsWith.Length < 1)
                {
                    _logger.LogWarning("Call to the API without any letter!!");
                    return null;
                }

                Cache.TryGetValue(EntityType.Alias, out IEnumerable<Domain.Models.Alias> aliases);

                result = aliases
                    .Where(a => a.VirtualName.IndexOf(startsWith.RemoveDiacritics(), StringComparison.CurrentCultureIgnoreCase) !=
                                -1).Take(3);

                _logger.LogInformation("END GetAliasesByName");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetAliasesByName(name={startsWith}) with message {ex.Message}");
            }
            finally
            {
                if (result == null)
                    _logger.LogWarning("No results for current request!!!");
            }
            return result;
        }


        [Route("bydrug/{drugId:int}")]
        public IEnumerable<Domain.Models.Alias> GetAliasesByDrug(int drugId)
        {
            IEnumerable<Domain.Models.Alias> result = null;
            try
            {
                _logger.LogInformation("BEGIN GetAliasesByDrug");
                result = LactafarmaService.GetAliasesByDrug(drugId);
                _logger.LogInformation("END GetAliasesByDrug");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetAliasesByDrug(drugId={drugId}) with message {ex.Message}");
            }
            finally
            {
                if (result == null)
                    _logger.LogWarning("No results for current request!!!");
            }

            return result;
        }

        [Route("{aliasId:int}")]
        public Domain.Models.Alias GetAlias(int aliasId)
        {
            Domain.Models.Alias result = null;
            try
            {
                _logger.LogInformation("BEGIN GetAlias");
                result = LactafarmaService.GetAlias(aliasId);
                _logger.LogInformation("END GetAlias");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetAlias(aliasId={aliasId}) with message {ex.Message}");
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