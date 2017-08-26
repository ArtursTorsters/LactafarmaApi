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
    public class DrugsController : BaseController, IDrugsController
    {
        #region Private Properties

        private readonly ILogger<DrugsController> _logger;

        #endregion

        #region Constructors

        public DrugsController(ILactafarmaService lactafarmaService, IMailService mailService,
            IConfigurationRoot config,
            ILogger<DrugsController> logger, IMemoryCache cache, UserManager<User> userManager) : base(
            lactafarmaService, mailService, config, cache, userManager)
        {
            _logger = logger;
            CacheInitialize(lactafarmaService.GetAllDrugs(), EntityType.Drug);
        }

        #endregion

        #region Public Methods

        [Route("byname/{startsWith}")]
        public IEnumerable<Domain.Models.Drug> GetDrugsByName(string startsWith)
        {
            IEnumerable<Domain.Models.Drug> result = null;
            try
            {
                _logger.LogInformation("BEGIN GetDrugsByName");

                if (startsWith.Length < 1)
                {
                    _logger.LogWarning("Call to the API without any letter!!");
                    return null;
                }

                Cache.TryGetValue(EntityType.Drug, out IEnumerable<Domain.Models.Drug> drugs);

                result = drugs
                    .Where(a => a.VirtualName.IndexOf(startsWith.RemoveDiacritics(),
                                    StringComparison.CurrentCultureIgnoreCase) !=
                                -1).Take(7);

                _logger.LogInformation("END GetDrugsByName");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetDrugsByName(name={startsWith}) with message {ex.Message}");
            }
            finally
            {
                if (result == null)
                    _logger.LogWarning("No results for current request!!!");
            }
            return result;
        }

        [Route("bygroup/{groupId:int}")]
        public IEnumerable<Domain.Models.Drug> GetDrugsByGroup(int groupId)
        {
            IEnumerable<Domain.Models.Drug> result = null;
            try
            {
                _logger.LogInformation("BEGIN GetDrugsByGroup");
                result = LactafarmaService.GetDrugsByGroup(groupId);
                _logger.LogInformation("END GetDrugsByGroup");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetDrugsByGroup(groupId={groupId}) with message {ex.Message}");
            }
            finally
            {
                if (result == null)
                    _logger.LogWarning("No results for current request!!!");
            }
            return result;
        }

        [Route("bybrand/{brandId:int}")]
        public IEnumerable<Domain.Models.Drug> GetDrugsByBrand(int brandId)
        {
            IEnumerable<Domain.Models.Drug> result = null;
            try
            {
                _logger.LogInformation("BEGIN GetDrugsByBrand");
                result = LactafarmaService.GetDrugsByBrand(brandId);
                _logger.LogInformation("END GetDrugsByBrand");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetDrugsByBrand(brandId={brandId}) with message {ex.Message}");
            }
            finally
            {
                if (result == null)
                    _logger.LogWarning("No results for current request!!!");
            }
            return result;
        }

        [Route("byalias/{aliasId:int}")]
        public Domain.Models.Drug GetDrugByAlias(int aliasId)
        {
            Domain.Models.Drug result = null;
            try
            {
                _logger.LogInformation("BEGIN GetDrugByAlias");
                result = LactafarmaService.GetDrugByAlias(aliasId);
                _logger.LogInformation("END GetDrugByAlias");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetDrugByAlias(aliasId={aliasId}) with message {ex.Message}");
            }
            finally
            {
                if (result == null)
                    _logger.LogWarning("No results for current request!!!");
            }
            return result;
        }

        [Route("{drugId:int}")]
        public Domain.Models.Drug GetDrug(int drugId)
        {
            Domain.Models.Drug result = null;
            try
            {
                _logger.LogInformation("BEGIN GetDrug");
                result = LactafarmaService.GetDrug(drugId);
                _logger.LogInformation("END GetDrug");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on JsonResult called GetDrug(drugId={drugId}) with message {ex.Message}");
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