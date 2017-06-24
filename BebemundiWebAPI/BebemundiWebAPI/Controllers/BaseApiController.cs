using BebemundiWebAPI.EntityFramework;
using BebemundiWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BebemundiWebAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        IBebemundiWebAPIRepository _repo;
        ModelFactory _modelFactory;

        public BaseApiController(IBebemundiWebAPIRepository repo)
        {
            _repo = repo;
        }

        protected IBebemundiWebAPIRepository Repository
        {
            get
            {
                return _repo;
            }
        }

        protected ModelFactory ModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request, Repository);
                }
                return _modelFactory;
            }
        }
    }
}