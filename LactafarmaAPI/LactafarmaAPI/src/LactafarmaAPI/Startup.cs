using System.IO;
using System.Security.Claims;
using LactafarmaAPI.Data;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Data.Interfaces;
using LactafarmaAPI.Data.Repositories;
using LactafarmaAPI.Services;
using LactafarmaAPI.Services.Interfaces;
using LactafarmaAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace LactafarmaAPI
{
    public class Startup
    {
        #region Private Properties

        private readonly IHostingEnvironment _env;

        #endregion

        #region Public Properties

        public IConfigurationRoot Configuration { get; }

        #endregion

        #region Constructors

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            env.ConfigureNLog("nlog.config");
        }

        #endregion

        #region Public Methods

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddSingleton(Configuration);

            if (_env.IsEnvironment("Development") || _env.IsEnvironment("Testing"))
                services.AddScoped<IMailService, DebugMailService>();

            //Entity Framework Core configuration
            services.AddEntityFrameworkSqlServer().AddDbContext<LactafarmaContext>(config =>
            {
                config.UseSqlServer(Configuration["ConnectionStrings:LactafarmaContextConnection"]);
            });

            //Configuration variables for NLog
            LogManager.Configuration.Variables["connectionString"] =
                Configuration["ConnectionStrings:LactafarmaContextConnection"];
            LogManager.Configuration.Variables["configDir"] = Directory.GetCurrentDirectory();

            //This interface is required for getting info from HttpContext (like Controller, Action, Url, UserAgent...)
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Repository collection used on LactafarmaApi
            services.AddScoped<IAliasRepository, AliasesRepository>();
            services.AddScoped<IAlertRepository, AlertsRepository>();
            services.AddScoped<IBrandRepository, BrandsRepository>();
            services.AddScoped<IDrugRepository, DrugsRepository>();
            services.AddScoped<IGroupRepository, GroupsRepository>();
            services.AddScoped<IUserRepository, UsersRepository>();
            services.AddScoped<ILogRepository, LogRepository>();

            //General service for retrieving items from EF Core
            services.AddScoped<ILactafarmaService, LactafarmaService>();

            //Allow logging system (ILogger)
            services.AddLogging();

            //Allow IMemoryCache mechanism
            services.AddMemoryCache();
            //Allo Session state
            services.AddSession();

            //Allow MVC services to be specified
            //Add AuthorizeFilter to demand the user to be authenticated in order to access resources.
            services.AddMvc(options => options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build())))
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            // Set up policies from claims
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser()
                        .RequireAssertion(context => context.User.HasClaim(ClaimTypes.Role, "Administrator"))
                        .Build();
                });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //NLog configuration
            loggerFactory.AddNLog();
            app.AddNLogWeb();

            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Error);
            }
            
            //app.UseDefaultFiles();
            app.UseStaticFiles();   

            app.UseMvc(config =>
            {
                //config.MapRoute(
                //    "Default",
                //    "{controller}/{action}/{id?}",
                //    new {controller = "Demo", action = "Index"});
            });
        }

        #endregion
    }
}