using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BebemundiWebAPI.EntityFramework;
using BebemundiWebAPI.Entities;
using BebemundiWebAPI.Models;
using System.Web.Http.Routing;

namespace BebemundiWebAPI.Controllers
{
    public class SearchController : BaseApiController
    {
        const int PAGE_SIZE = 25;

        public SearchController(IBebemundiWebAPIRepository repo)
            : base(repo)
        {            
        }

        public IHttpActionResult Get(int page = 0)
        {
            var baseQuery = Repository.Search();

            var totalCount = baseQuery.Count();
            var totalPages = Math.Ceiling((double)totalCount / PAGE_SIZE);

            var helper = new UrlHelper(Request);

            var prevUrl = page > 0 ? helper.Link("DefaultPattern", new { page = page - 1 }) : "";
            var nextUrl = page < totalPages - 1 ? helper.Link("DefaultPattern", new { page = page + 1 }) : "";

            var results = baseQuery.OrderBy(r => r.Nombre).Skip(PAGE_SIZE * page)
                                   .Take(PAGE_SIZE)
                                   .ToList()
                                   .Select(f => ModelFactory.Create(f));

            if (results == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                TotalCount = totalCount,
                TotalPage = totalPages,
                PrevPageUrl = prevUrl,
                NextPageUrl = nextUrl,
                Results = results
            });
        }

        public IHttpActionResult GetByAlias(int page = 0)
        {
            var baseQuery = Repository.SearchByAlias();

            var totalCount = baseQuery.Count();
            var totalPages = Math.Ceiling((double)totalCount / PAGE_SIZE);

            var helper = new UrlHelper(Request);

            var prevUrl = page > 0 ? helper.Link("DefaultPattern", new { page = page - 1 }) : "";
            var nextUrl = page < totalPages - 1 ? helper.Link("DefaultPattern", new { page = page + 1 }) : "";

            var results = baseQuery.OrderBy(r => r.Nombre).Skip(PAGE_SIZE * page)
                                   .Take(PAGE_SIZE)
                                   .ToList()
                                   .Select(f => ModelFactory.Create(f));

            if (results == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                TotalCount = totalCount,
                TotalPage = totalPages,
                PrevPageUrl = prevUrl,
                NextPageUrl = nextUrl,
                Results = results
            });
        }

        public IHttpActionResult GetByTrademark(int page = 0)
        {
            var baseQuery = Repository.SearchByTrademark();

            var totalCount = baseQuery.Count();
            var totalPages = Math.Ceiling((double)totalCount / PAGE_SIZE);

            var helper = new UrlHelper(Request);

            var prevUrl = page > 0 ? helper.Link("DefaultPattern", new { page = page - 1 }) : "";
            var nextUrl = page < totalPages - 1 ? helper.Link("DefaultPattern", new { page = page + 1 }) : "";

            var results = baseQuery.OrderBy(r => r.Nombre).Skip(PAGE_SIZE * page)
                                   .Take(PAGE_SIZE)
                                   .ToList()
                                   .Select(f => ModelFactory.Create(f));

            if (results == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                TotalCount = totalCount,
                TotalPage = totalPages,
                PrevPageUrl = prevUrl,
                NextPageUrl = nextUrl,
                Results = results
            });
        }

        public IHttpActionResult GetByProduct(int page = 0)
        {
            var baseQuery = Repository.SearchByProduct();

            var totalCount = baseQuery.Count();
            var totalPages = Math.Ceiling((double)totalCount / PAGE_SIZE);

            var helper = new UrlHelper(Request);

            var prevUrl = page > 0 ? helper.Link("DefaultPattern", new { page = page - 1 }) : "";
            var nextUrl = page < totalPages - 1 ? helper.Link("DefaultPattern", new { page = page + 1 }) : "";

            var results = baseQuery.OrderBy(r => r.Nombre).Skip(PAGE_SIZE * page)
                                   .Take(PAGE_SIZE)
                                   .ToList()
                                   .Select(f => ModelFactory.Create(f));

            if (results == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                TotalCount = totalCount,
                TotalPage = totalPages,
                PrevPageUrl = prevUrl,
                NextPageUrl = nextUrl,
                Results = results
            });
        }
    }
}