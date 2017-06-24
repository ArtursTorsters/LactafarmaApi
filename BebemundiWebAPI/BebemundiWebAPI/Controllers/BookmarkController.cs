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
using System.Threading;
using BebemundiWebAPI.Services;
using BebemundiWebAPI.Models;
using System.Web.Http.Routing;
using BebemundiWebAPI.Filters;

namespace BebemundiWebAPI.Controllers
{
    [BebemundiAuthorize]
    public class BookmarkController : BaseApiController
    {
        const int PAGE_SIZE = 10;

        private IBebemundiWebAPIIdentityService _identityService;        
        public BookmarkController(IBebemundiWebAPIRepository repo, 
            IBebemundiWebAPIIdentityService identityService):base(repo)
        {
            _identityService = identityService;
        }

        public IHttpActionResult GetByUser(int page = 0)
        {
            var username = _identityService.CurrentUser;            

            var baseQuery = Repository.GetBookmarksByUser(username).OrderBy(p => p.Id);

            var totalCount = baseQuery.Count();
            var totalPages = Math.Ceiling((double)totalCount / PAGE_SIZE);

            var helper = new UrlHelper(Request);

            var prevUrl = page > 0 ? helper.Link("DefaultPattern", new { page = page - 1 }) : "";
            var nextUrl = page < totalPages - 1 ? helper.Link("DefaultPattern", new { page = page + 1 }) : "";

            var results = baseQuery.Skip(PAGE_SIZE * page)
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

        public IHttpActionResult GetLastId()
        {
            var results = Repository.GetLastIdBookmark();
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        public IHttpActionResult Post([FromBody]BookmarkModel bookmark)
        {
            try
            {
                var entity = ModelFactory.Parse(bookmark);

                if (entity == null) BadRequest( "Could not read bookmark entry in body");

                // Make sure it's not duplicated
                if (Repository.GetBookmark(entity.Id) != null)
                {
                    return BadRequest( "Duplicate Bookmark not allowed.");
                }

                if (Repository.InsertBookmark(entity) && Repository.SaveAll())
                {
                    return Created<BookmarkModel>("Bookmark", ModelFactory.Create(entity));
                }
                else
                {
                    return BadRequest( "Could not save to the database.");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(string id)
        {
            try
            {
                if (Repository.GetBookmarksByUser(_identityService.CurrentUser).Any(e => e.Id == id) == false)
                {
                    return NotFound();
                }

                if (Repository.DeleteBookmark(id) && Repository.SaveAll())
                {
                    return Ok(HttpStatusCode.OK);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}