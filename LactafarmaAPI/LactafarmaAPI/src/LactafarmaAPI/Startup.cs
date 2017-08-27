using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using LactafarmaAPI.Data;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Data.Interfaces;
using LactafarmaAPI.Data.Repositories;
using LactafarmaAPI.Services;
using LactafarmaAPI.Services.Interfaces;
using LactafarmaAPI.Services.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
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
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;

namespace LactafarmaAPI
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        #region Private Properties

        private readonly IHostingEnvironment _env;

        #endregion

        #region Public Properties

        /// <summary>
        /// IConfigurationRoot
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="env"></param>
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


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddSingleton(Configuration);

            //TODO: Create Real Mail Service for sending to emails for administrator role
            //if (_env.IsEnvironment("Development") || _env.IsEnvironment("Testing"))
            services.AddSingleton<IMailService, MailSenderService>();

            // EF Core Identity
            services.AddIdentity<User, IdentityRole>(config =>
            {
                // Password settings
                config.Password.RequireDigit = true;
                config.Password.RequiredLength = 8;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = true;
                config.Password.RequireLowercase = false;

                // Lockout settings
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                config.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                config.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<LactafarmaContext>().AddDefaultTokenProviders();

            // Cookie settings
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/auth/login";
                config.LogoutPath = "/auth/logout";
                config.ExpireTimeSpan = TimeSpan.FromDays(150);
            });

            //Handle AuthenticationEvents on API calls (401 message)
            services.ConfigureApplicationCookie(config =>
            {
                config.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = async ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        await Task.Yield();
                    }
                };
            });

            // Adding custom properties to ClaimPrincipal (LanguageId) - Extend the default one: HttpContext.User
            services.AddScoped<IUserClaimsPrincipalFactory<User>, AppClaimsPrincipalFactory>();

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
            //services.AddScoped<IUserRepository, UsersRepository>();
            services.AddScoped<ILogRepository, LogRepository>();

            //General service for retrieving items from EF Core
            services.AddScoped<ILactafarmaService, LactafarmaService>();
                     
            //Allow logging system (ILogger)
            services.AddLogging();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "LactafarmaApi",
                    Description = "API for getting interesting information about drugs for breastfeeding, also it will be used as template for ASP.NET Core projects with Entity Framework Core (2.0)",
                    Version = "v1",
                    Contact = new Contact { Email = "xpertpoint.solutions@gmail.com", Url = "https://github.com/gomnet/lactafarma/issues" }
                });

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "LactafarmaAPI.xml");
                c.IncludeXmlComments(filePath);
            });

            services.ConfigureSwaggerGen(
                options => {
                    // UseFullTypeNameInSchemaIds replacement for .NET Core
                    options.CustomSchemaIds(x => x.FullName);                    
                    });

            //Allow MVC services to be specified
            //Add AuthorizeFilter to demand the user to be authenticated in order to access resources.
            services.AddMvc(options =>
            {
                //TODO: SSL requirement on Hosting
                //if (_env.IsProduction())
                //{
                //    options.Filters.Add(new RequireHttpsAttribute());
                //}
                //options.Filters
                //    .Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                //        .Build()));
            }).AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

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

            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            services.AddSession();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
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

            app.UseAuthentication();

            app.UseSession();

            //Specify route template for API documentation
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "help/{documentName}/lactafarma.json";
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);
            });

            //Indicate endpoints/documents for Swagger UI generator
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "help";
                c.SwaggerEndpoint("/help/v1/lactafarma.json", "LactafarmaApi V1");
                c.InjectStylesheet("/css/themes/theme-monokai.css");
                c.InjectOnCompleteJavaScript("/js/custom-swagger-ui.js");
            });

            app.UseMvc();            
        }

        #endregion
    }
}