﻿using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Trade_MVC6.Models.B2BStrore;
using Trade_MVC6.Models.Identity.AccountDetails;

namespace Trade_MVC6.Models.Identity
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

                var context = app.ApplicationServices.GetService<B2BDbContext>();
                    var contact = new ContactRecord
                    {
                        CreationAuthor = "System",
                        CreationDate = DateTime.Now,
                        LastEditDate = DateTime.Now,
                        LastEditor = "System"
                    };
                context.Add(contact);
                adminUser.Contact = contact;
                userManager.CreateAsync(adminUser, "123456").Wait();


                userManager.AddToRoleAsync(adminUser, Roles.Admin).Wait();
                }

            }
        }
    }
