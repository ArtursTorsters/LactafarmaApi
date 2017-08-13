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
    public class GroupsController : BaseController, IGroupsController
    {
        #region Private Properties

        private readonly ILogger<GroupsController> _logger;

        #endregion

        #region Constructors

        public GroupsController(ILactafarmaService lactafarmaService, IMailService mailService,
            IConfigurationRoot config,
            ILogger<GroupsController> logger, IMemoryCache cache) : base(lactafarmaService, mailService, config, cache)
        {
            _logger = logger;

            CacheInitialize(lactafarmaService.GetAllGroups(), EntityType.Group);
        }

        #endregion

        #region Public Methods

        [Route("byname/{startsWith:string}")]
        public JsonResult GetGroupsByName(string startsWith)
        {
            var result = new JsonResult("");
            if (startsWith.Length < 1) return result;

            Cache.TryGetValue(EntityType.Group, out IEnumerable<BaseModel> groups);

            result = Json(groups
                .Where(a => a.VirtualName.IndexOf(startsWith, StringComparison.CurrentCultureIgnoreCase) !=
                            -1).Take(3));

            return result;
        }

        [Route("{groupId:int}")]
        public JsonResult GetGroup(int groupId)
        {
            var result = new JsonResult(string.Empty);
            try
            {
                _logger.LogInformation("BEGIN GetGroup");
                result = Json(LactafarmaService.GetGroup(groupId));
                _logger.LogInformation("END GetGroup");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception on JsonResult called GetGroup(groupId={groupId}) with message {ex.Message}");
            }

            return result;
        }

        #endregion
    }
}