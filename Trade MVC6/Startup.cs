using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Trade_MVC6.Models.AutoMapper;
using Trade_MVC6.Models.B2BStrore;
using Trade_MVC6.Models.Identity;
using Trade_MVC6.Services;
using Trade_MVC6.Services.EmailSender;
using Trade_MVC6.Services.JsonSerializer;
using Trade_MVC6.Services._1С;
using Trade_MVC6.Services._1С.Providers;

namespace Trade_MVC6
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
                .AddDbContext<B2BDbContext>(opt =>
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
            .AddEntityFrameworkStores<B2BDbContext>()
            .AddDefaultTokenProviders();

            // Configure Identity service
            services.Configure<IdentityOptions>(setup =>
            {
                setup.Cookies.ApplicationCookie.LoginPath = new Microsoft.AspNet.Http.PathString("/Account/Login");
                setup.Cookies.ApplicationCookie.LogoutPath = new Microsoft.AspNet.Http.PathString("/Account/LogOff");
                setup.Cookies.ApplicationCookie.AccessDeniedPath =
                    new Microsoft.AspNet.Http.PathString("/Account/AccessDenied");
                setup.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(1);
            });

            // Antiforgery RC1 have Bug

            services.ConfigureAntiforgery(options =>
            {
                options.CookieName = Configuration["AntiForgery:CookieName"];
                options.RequireSsl = false;
            });


            // Add framework services.
            services.AddMvc(setup =>
            {
                //setup.Filters.Add()
            });

            // Configure AutoMapper
            services.MapperConfigure();

            // Configure SMTP Robot
            services.AddInstance<IEmailSender>(new EmailSimpleSender(Configuration));

            //Configure Serializer
            services.AddInstance<IJsonSerializer>(new SimpleSerializer());

            //Configure 1C Provider
            services.AddInstance<IProvider1C>(new Provider1C(new ProviderUsers1C(), null, null));

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
                } else
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
            // Check For IsAdmin User
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

                    routes.MapRoute(
                    name: "notFound",
                    template: "{*url}",
                    defaults: new
                        {
                            controller = "Home",
                            action = "Index"
                        });

                });

            // Set Default Katana Module
            // app.Properties["builder.DefaultApp"] = defaultModule;

            }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
        }
    }
