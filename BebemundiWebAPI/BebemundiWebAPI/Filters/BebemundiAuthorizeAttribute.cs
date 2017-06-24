using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Text;
using System.Security.Principal;
using System.Threading;
using WebMatrix.WebData;
using BebemundiWebAPI.EntityFramework;
using Ninject;

namespace BebemundiWebAPI.Filters
{
    public class BebemundiAuthorizeAttribute : AuthorizationFilterAttribute
    {
        private bool _perUser;
        public BebemundiAuthorizeAttribute(bool perUser = true)
        {
            _perUser = perUser;
        }

        [Inject]
        public BebemundiWebAPIRepository Repository { get; set; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            const string APIKEYNAME = "apikey";
            const string TOKENNAME = "token";

            var query = HttpUtility.ParseQueryString(actionContext.Request.RequestUri.Query);

            if (!string.IsNullOrWhiteSpace(query[APIKEYNAME]) &&
              !string.IsNullOrWhiteSpace(query[TOKENNAME]))
            {

                var apikey = query[APIKEYNAME].Replace(" ", "+");
                var token = query[TOKENNAME].Replace(" ", "+"); 

                var authToken = Repository.GetAuthToken(token);
                if (authToken != null)
                {
                    var user = Repository.GetUser(authToken.UserId);

                    if (user.AppId == apikey && authToken.Expiration > DateTime.UtcNow)
                    {
                        if (_perUser)
                        {
                            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
                            {
                                return;
                            }

                            var authHeader = actionContext.Request.Headers.Authorization;

                            if (authHeader != null)
                            {
                                if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                                    !string.IsNullOrWhiteSpace(authHeader.Parameter))
                                {
                                    var rawCredentials = authHeader.Parameter;
                                    var encoding = Encoding.GetEncoding("iso-8859-1");
                                    var credentials = encoding.GetString(Convert.FromBase64String(rawCredentials));
                                    var split = credentials.Split(':');
                                    var username = split[0];
                                    var password = split[1];

                                    if (!WebSecurity.Initialized)
                                    {
                                        WebSecurity.InitializeDatabaseConnection("StringConnection", "UserProfile",
                                            "UserId", "UserName", autoCreateTables: true);
                                    }

                                    if (WebSecurity.Login(username, password))
                                    {
                                        var principal = new GenericPrincipal(new GenericIdentity(username), null);
                                        Thread.CurrentPrincipal = principal;
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }

            HandleUnauthorized(actionContext);
        }

        private void HandleUnauthorized(HttpActionContext actionContext)
        {
            //TODO: location parameter should be configurable
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            if (_perUser)
                actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='Bebemundi' location='http://localhost:53121/account/login'");
        }
    }
}