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
using System.Web.Http.Cors;
using System.Text;

namespace BebemundiWebAPI.Controllers
{
    public class TokenController : BaseApiController
    {              
        public TokenController(IBebemundiWebAPIRepository repo)
            : base(repo)
        {            
        }

        public IHttpActionResult Post([FromBody]TokenRequestModel model)
        {
            try
            {
                var user = Repository.GetApiUsers().Where(u => u.AppId == model.ApiKey).FirstOrDefault();
                if (user != null)
                {
                    var secret = user.Secret;

                    //TODO: Change before to go online!!
                    // Simplistic implementation DO NOT USE
                    var key = Convert.FromBase64String(secret);
                    var provider = new System.Security.Cryptography.HMACSHA256(key);
                    // Compute Hash from API Key (NOT SECURE)
                    var hash = provider.ComputeHash(Encoding.UTF8.GetBytes(user.AppId));
                    var signature = Convert.ToBase64String(hash);

                    if (signature == model.Signature)
                    {
                        var rawTokenInfo = string.Concat(user.AppId + DateTime.UtcNow.ToString("d"));
                        var rawTokenByte = Encoding.UTF8.GetBytes(rawTokenInfo);
                        var token = provider.ComputeHash(rawTokenByte);
                        var authToken = new AuthToken()
                        {
                            Token = Convert.ToBase64String(token),
                            Expiration = DateTime.UtcNow.AddDays(7),
                            UserId = user.UserId
                        };
                        if (Repository.InsertToken(authToken) && Repository.SaveAll())
                        {
                            return Created<AuthTokenModel>("Token", ModelFactory.Create(authToken));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest();
        }
    }

}