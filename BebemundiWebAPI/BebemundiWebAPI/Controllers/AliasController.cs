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
    public class AliasController : BaseApiController
    {
        const int PAGE_SIZE = 25;

        public AliasController(IBebemundiWebAPIRepository repo) : base(repo)
        {
        }

        public IHttpActionResult Get(int page = 0)
        {
            var baseQuery = Repository.GetAllAliases().OrderBy(p => p.Nombre);

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
            var baseQuery = Repository.FindAliasesByNameStartsWith(parameter).OrderBy(p => p.Nombre);

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

        public IHttpActionResult GetByGroup(string parameter, int page = 0)
        {           
            var baseQuery = Repository.GetAliasesByProduct(parameter).OrderBy(p => p.Nombre);

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

        public IHttpActionResult GetAlias(string parameter)
        {
            var results = Repository.GetAlias(parameter);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(ModelFactory.Create(results));                     
        }

        public IHttpActionResult GetLastId()
        {
            var results = Repository.GetLastIdAlias();
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        //For POST calls we need to implement JSON object another side in order to map with the header of the method (CRT implementation)
        public IHttpActionResult Post([FromBody]AliasModel alias)
        {
            try
            {
                var entity = ModelFactory.Parse(alias);

                if (entity == null) BadRequest( "Could not read alias entry in body");

                // Make sure it's not duplicated
                if (Repository.GetAlias(entity.Id) != null)
                {
                    return BadRequest( "Duplicate Alias not allowed.");
                }

                if (Repository.InsertAlias(entity) && Repository.SaveAll())
                {
                    return Created<AliasModel>("Alias", ModelFactory.Create(entity));
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
        public IHttpActionResult Patch(string id, [FromBody] AliasModel alias)
        {
            try
            {
                var entity = Repository.GetAlias(id);
                if (entity == null) return NotFound();

                var parsedValue = ModelFactory.Parse(alias);
                if (parsedValue == null) return Ok(HttpStatusCode.BadRequest);

                if (entity.IdMedicamento != parsedValue.IdMedicamento)
                    entity.IdMedicamento = parsedValue.IdMedicamento;
                if (entity.Nombre != parsedValue.Nombre)
                    entity.Nombre = parsedValue.Nombre;                

                if (Repository.UpdateAlias(entity) && Repository.SaveAll())
                    return Ok();


                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message);
            }
        }
    }
}