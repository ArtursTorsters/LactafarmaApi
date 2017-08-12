using LactafarmaAPI.Data;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Data.Interfaces;
using LactafarmaAPI.Data.Repositories;
using LactafarmaAPI.Services;
using LactafarmaAPI.Services.Interfaces;
using LactafarmaAPI.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

            if (env.IsEnvironment("Development"))
                builder.AddApplicationInsightsSettings(true);

            Configuration = builder.Build();
        }

        #endregion

        #region Public Methods

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddSingleton(Configuration);

            if (_env.IsEnvironment("Development") || _env.IsEnvironment("Testing"))
                services.AddScoped<IMailService, DebugMailService>();

            services.AddEntityFrameworkSqlServer().AddDbContext<LactafarmaContext>(config =>
            {
                config.UseSqlServer(Configuration["ConnectionStrings:LactafarmaContextConnection"]);
            });

            services.AddScoped<IAliasRepository, AliasesRepository>();
            services.AddScoped<IAlertRepository, AlertsRepository>();
            services.AddScoped<IBrandRepository, BrandsRepository>();
            services.AddScoped<IDrugRepository, DrugsRepository>();
            services.AddScoped<IGroupRepository, GroupsRepository>();
            services.AddScoped<IUserRepository, UsersRepository>();

            services.AddScoped<ILactafarmaService, LactafarmaService>();

            services.AddLogging();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Error);
            }
            //app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc(config =>
            {
                config.MapRoute(
                    "Default",
                    "{controller}/{action}/{id?}",
                    new {controller = "Demo", action = "Index"});
            });
        }

        #endregion
    }
}