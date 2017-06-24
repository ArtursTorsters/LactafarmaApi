using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BebemundiWebAPI.EntityFramework;

namespace BebemundiWebAPI.Controllers
{
    [RoutePrefix("api/lactafarma/stats")]
    public class StatsController : BaseApiController
    {
        public StatsController(IBebemundiWebAPIRepository repo): base(repo)
        {
            
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            var results = new
            {
                NumProducts = Repository.GetAllProducts().Count(),
                NumAliases = Repository.GetAllAliases().Count(),
                NumTrademarks = Repository.GetAllTrademarks().Count(),
                NumGroups = Repository.GetAllGroups().Count(),
                NumUsers = Repository.GetApiUsers().Count()
            };

            return Ok(results);
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            if (id == 1)
                return Ok(new {NumProducts = Repository.GetAllProducts().Count()});

            if (id == 2)
                return Ok(new { NumProducts = Repository.GetAllAliases().Count() });

            if (id == 3)
                return Ok(new { NumProducts = Repository.GetAllTrademarks().Count() });

            if (id == 4)
                return Ok(new { NumProducts = Repository.GetAllGroups().Count() });

            if (id == 5)
                return Ok(new { NumProducts = Repository.GetApiUsers().Count() });

            return NotFound();
        }

        [Route("{name:alpha}")]
        public IHttpActionResult Get(string name)
        {
            if (name == "products")
                return Ok(new { NumProducts = Repository.GetAllProducts().Count() });

            if (name == "aliases")
                return Ok(new { NumProducts = Repository.GetAllAliases().Count() });

            if (name == "trademarks")
                return Ok(new { NumProducts = Repository.GetAllTrademarks().Count() });

            if (name == "groups")
                return Ok(new { NumProducts = Repository.GetAllGroups().Count() });

            if (name == "users")
                return Ok(new { NumProducts = Repository.GetApiUsers().Count() });

            return NotFound();
        }
    }
}
