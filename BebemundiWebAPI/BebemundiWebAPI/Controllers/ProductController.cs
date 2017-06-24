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
    public class ProductController : BaseApiController
    {
        const int PAGE_SIZE = 25;

        public ProductController(IBebemundiWebAPIRepository repo): base(repo)
        {            
        }

        public IHttpActionResult Get(int page = 0)
        {                      
            var baseQuery = Repository.GetAllProducts().OrderBy(p => p.Nombre);

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

        public IHttpActionResult Get(string parameter, int page = 0)
        {           
            var baseQuery = Repository.FindProductsByNameStartsWith(parameter).OrderBy(p => p.Nombre);

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
            var baseQuery = Repository.GetProductsByGroup(parameter).OrderBy(p => p.Nombre);

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

        public IHttpActionResult GetByProduct(string parameter, int page = 0)
        {           
            var baseQuery = Repository.GetProductsByProduct(parameter).OrderBy(p => p.Nombre);

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

        public IHttpActionResult GetProduct(string parameter)
        {
            var results = Repository.GetProduct(parameter);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(ModelFactory.Create(results));              
        }

        public IHttpActionResult GetLastId()
        {
            var results = Repository.GetLastIdProduct();
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        //For POST calls we need to implement JSON object another side in order to map with the header of the method (CRT implementation)
        public IHttpActionResult Post([FromBody]ProductModel product)
        {
            try
            {
                var entity = ModelFactory.Parse(product);

                if (entity == null) BadRequest( "Could not read product entry in body");

                // Make sure it's not duplicated
                if (Repository.GetProduct(entity.Id) != null)
                {
                    return BadRequest( "Duplicate Product not allowed.");
                }

                if (Repository.InsertProduct(entity) && Repository.SaveAll())
                {
                    return Created<ProductModel>("Product", ModelFactory.Create(entity));
                }
                else
                {
                    return BadRequest("Could not save to the database.");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [HttpPatch]
        public IHttpActionResult Patch(string id, [FromBody] ProductModel product)
        {
            try
            {
                var entity = Repository.GetProduct(id);
                if (entity == null) return NotFound();

                var parsedValue = ModelFactory.Parse(product);
                if (parsedValue == null) return Ok(HttpStatusCode.BadRequest);

                if (entity.Fecha != parsedValue.Fecha)
                    entity.Fecha = parsedValue.Fecha;
                if (entity.Comentario != parsedValue.Comentario)
                    entity.Comentario = parsedValue.Comentario;
                if (entity.DescripcionRiesgo != parsedValue.DescripcionRiesgo)
                    entity.DescripcionRiesgo = parsedValue.DescripcionRiesgo;
                if (entity.IdGrupo != parsedValue.IdGrupo)
                    entity.IdGrupo = parsedValue.IdGrupo;
                if (entity.Nombre != parsedValue.Nombre)
                    entity.Nombre = parsedValue.Nombre;
                if (entity.Riesgo != parsedValue.Riesgo)
                    entity.Riesgo = parsedValue.Riesgo;                

                if (Repository.UpdateProduct(entity) && Repository.SaveAll())
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