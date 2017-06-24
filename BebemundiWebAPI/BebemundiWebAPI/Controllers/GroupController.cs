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
    public class GroupController : BaseApiController
    {
        const int PAGE_SIZE = 25;

        public GroupController(IBebemundiWebAPIRepository repo): base(repo)
        {            
        }

        public IHttpActionResult Get(int page = 0)
        {                     
            var baseQuery = Repository.GetAllGroups().OrderBy(p => p.Nombre);

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

            return Ok(new { 
                TotalCount = totalCount,
                TotalPage = totalPages,
                PrevPageUrl = prevUrl,
                NextPageUrl = nextUrl,
                Results = results
            });
        }

        public IHttpActionResult Get(string parameter, int page = 0)
        {           
            var baseQuery = Repository.FindGroupsByNameStartsWith(parameter).OrderBy(p => p.Nombre);

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

        public IHttpActionResult GetGroup(string parameter)
        {
            var results = Repository.GetGroup(parameter);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(ModelFactory.Create(results));                           
        }

        public IHttpActionResult GetLastId()
        {
            var results = Repository.GetLastIdGroup();
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        public IHttpActionResult Post([FromBody]GroupModel group)
        {
            try
            {
                var entity = ModelFactory.Parse(group);

                if (entity == null) BadRequest( "Could not read group entry in body");

                // Make sure it's not duplicated
                if (Repository.GetGroup(entity.Id) != null)
                {
                    return BadRequest( "Duplicate Group not allowed.");
                }

                if (Repository.InsertGroup(entity) && Repository.SaveAll())
                {
                    return Created<GroupModel>("Group", ModelFactory.Create(entity));
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

        [HttpPut]
        [HttpPatch]
        public IHttpActionResult Patch(string id, [FromBody] GroupModel group)
        {
            try
            {
                var entity = Repository.GetGroup(id);
                if (entity == null) return NotFound();

                var parsedValue = ModelFactory.Parse(group);
                if (parsedValue == null) return Ok(HttpStatusCode.BadRequest);

                if (entity.Fecha != parsedValue.Fecha)
                    entity.Fecha = parsedValue.Fecha;                
                if (entity.Nombre != parsedValue.Nombre)
                    entity.Nombre = parsedValue.Nombre;                

                if (Repository.UpdateGroup(entity) && Repository.SaveAll())
                    return Ok();


                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}