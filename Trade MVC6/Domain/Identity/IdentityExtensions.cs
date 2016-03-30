using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Trade_MVC5.Domain.EF;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Trade_MVC5.Domain.Identity
    {
    public static class IdentityExtensions
        {
        public static void EnsureRolesCreated(this IApplicationBuilder app)
            {
            var context = app.ApplicationServices.GetServices<ApplicationDbContext>();
            //if (context.AllMigrationsApplied())
            //{

            //}
            var roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();
            foreach (var role in Roles.All)
                {
                if (!roleManager.RoleExistsAsync(role.ToUpper()).Result)
                    {
                    roleManager.CreateAsync(new IdentityRole { Name = role }).Wait();
                    }

                }
            }

        public static void EnsureRootCreated(this IApplicationBuilder app)
            {
            var userManager = app.ApplicationServices.GetService<UserManager<ApplicationUser>>();

            if (userManager.FindByNameAsync(ApplicationUser.Admin.UserName).Result == null)
                {
                var adminUser = ApplicationUser.Admin;

                 // Possible need to check IdentityResult
                 userManager.CreateAsync(adminUser, "123456").Wait();
                 userManager.AddToRoleAsync(adminUser, Roles.Admin).Wait();
                }

            }
        }
    }
