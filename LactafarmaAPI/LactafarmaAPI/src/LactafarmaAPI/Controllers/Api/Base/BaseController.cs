using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LactafarmaAPI.Domain.Models.Base;
using LactafarmaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LactafarmaAPI.Controllers.Api.Base
{
    [Route("api/v1/[controller]")]
    public class BaseController: Controller
    {
        public readonly IConfigurationRoot Config;
        public readonly ILactafarmaService LactafarmaService;
        public readonly IMailService MailService;
        public IMemoryCache Cache { get; set; }

        public enum EntityType
        {
            Alert,
            Alias,
            Drug,
            Brand,
            Group,
            User
        }

        public BaseController(ILactafarmaService lactafarmaService, IMailService mailService, IConfigurationRoot config, IMemoryCache cache)
        {
            LactafarmaService = lactafarmaService;
            MailService = mailService;
            Config = config;
            Cache = cache;
        }


        public void CacheInitialize<TModel>(IEnumerable<TModel> items, EntityType type) where TModel : BaseModel
        {
            // Set cache options.
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Keep in cache on same state, NeverRemove
                .SetPriority(CacheItemPriority.NeverRemove)
                .SetSlidingExpiration(TimeSpan.FromDays(7));

            IEnumerable<TModel> cacheEntries;

            if (!Cache.TryGetValue(type, out cacheEntries))
            {
                // Key not in cache, so get data.
                cacheEntries = items;

                // Save data in cache.
                Cache.Set(type, cacheEntries, cacheEntryOptions);
            }
        }

        /// <summary>
        /// Clear set of objects stored on IMemoryCache
        /// </summary>
        public async Task ClearCachesAsync()
        {
            await Task.Run(() => Cache.Remove(EntityType.Alias));
            await Task.Run(() => Cache.Remove(EntityType.Drug));
            await Task.Run(() => Cache.Remove(EntityType.Group));
            await Task.Run(() => Cache.Remove(EntityType.Brand));
         }
    }
}
