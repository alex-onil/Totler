using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Entity;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Trade_MVC6.Models.EF;
using Trade_MVC6.Models.Identity;

namespace Trade_MVC5
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add EF service
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(opt =>
                    opt.UseSqlServer(Configuration["ConnectionStrings:TempDB"]));

            //Configure Identity middleware with ApplicationUser and the EF7 IdentityDbContext
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = false;
                config.Password.RequireDigit = false;
                config.Password.RequiredLength = 1;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonLetterOrDigit = false;
                config.Password.RequireUppercase = false;

            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Configure Identity service
            services.Configure<IdentityOptions>(setup =>
            {
                setup.Cookies.ApplicationCookie.LoginPath = new Microsoft.AspNet.Http.PathString("/Account/Login");
                setup.Cookies.ApplicationCookie.LogoutPath = new Microsoft.AspNet.Http.PathString("/Account/LogOff");
                setup.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(1);
            });

            // Add framework services.
            services.AddMvc(setup =>
            {
                //setup.Filters.Add()
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            //var test = Configuration.Get<string>("ConnectionStrings:TempDB");

            //Debug.Write(Configuration.Get<string>("ConnectionStrings:TempDB"));
            
            //app.Use(typeof(Katana_Test.KatanaTest), "Log Module >");
            // app.UseMiddleware<Katana_Test.KatanaMiddlewareTest>();

            app.UseStaticFiles();

            // Using Identity Engine
            app.UseIdentity();

            // Check For System Groups
            app.EnsureRolesCreated();
            // Check For Admin User
            app.EnsureRootCreated();

            app.UseMvc(routes =>
                {
                    //  Area Registration
                    routes.MapRoute(name: "areaRoute",
                    template: "{area:exists}/{controller}/{action}",
                    defaults: new { controller = "Home", action = "Index" });

                    routes.MapRoute(
                    name: "Home Page",
                    template: "Home/{*options}",
                    defaults: new { controller = "Home", action = "Index" });

                    routes.MapRoute(
                     name: "default",
                     template: "{controller=Home}/{action=Index}/{id?}");
                 });

            // Set Default Katana Module
            // app.Properties["builder.DefaultApp"] = defaultModule;

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
