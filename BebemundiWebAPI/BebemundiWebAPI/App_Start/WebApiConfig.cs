using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using BebemundiWebAPI.Services;
using CacheCow.Server.EntityTagStore.SqlServer;
using Newtonsoft.Json.Serialization;
using BebemundiWebAPI.Filters;
//using WebApiContrib.Formatting.Jsonp;
using System.Net.Http.Formatting;
using CacheCow.Server;

namespace BebemundiWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Identifiers",
                routeTemplate: "api/lactafarma/{controller}/get/{id}"                
            );            

            config.Routes.MapHttpRoute(
                name: "DefaultPattern",
                routeTemplate: "api/lactafarma/{controller}/{action}/{parameter}",
                defaults: new { parameter = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                  name: "Token",
                  routeTemplate: "api/lactafarma/token",
                  defaults: new { controller = "token" }
            );
            

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            //config.EnableSystemDiagnosticsTracing();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Add support CORS
            var attr = new EnableCorsAttribute("http://www.bebemundi.com", "*", "*");
            config.EnableCors(attr);    

            //Add support JSONP (Better to use CORS = Cross Origin Resource Sharing
            /*var formatter = new JsonpMediaTypeFormatter(jsonFormatter, "cb");
            config.Formatters.Insert(0, formatter);*/

            // Replace the Controller Configuration
            config.Services.Replace(typeof(IHttpControllerSelector),
              new BebemundiWebAPIControllerSelector(config));

            //Congure Caching/ETag Support
            var connString = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
            var etagStore = new SqlServerEntityTagStore(connString);
            var cacheHandler = new CachingHandler(config, etagStore) { AddLastModifiedHeader = false };
            config.MessageHandlers.Add(cacheHandler);

#if !DEBUG
    // Force HTTPS on entire API
    //config.Filters.Add(new RequireHttpsAttribute());
#endif
        }
    }
}
