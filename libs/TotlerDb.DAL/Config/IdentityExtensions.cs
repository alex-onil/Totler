using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using TotlerRepository.Models.Identity;

namespace TotlerDb.DAL.Config
    {
    public static class IdentityExtensions
        {
        #region Configuration

        private static RoleManager<IdentityRole> _roleManager;
        private static UserManager<ApplicationUser> _userManager;

        public static void EnsureRolesCreated(this IApplicationBuilder app)
            {
            var context = app.ApplicationServices.GetServices<AppDbContext>();
            //if (context.AllMigrationsApplied())
            //{

            //}
            _roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();

            foreach (var role in Roles.All)
                {
                if (!_roleManager.RoleExistsAsync(role.ToUpper()).Result)
                    {
                    _roleManager.CreateAsync(new IdentityRole { Name = role }).Wait();
                    }

                }
            }

        public static void EnsureRootCreated(this IApplicationBuilder app)
            {
            _userManager = app.ApplicationServices.GetService<UserManager<ApplicationUser>>();


            if (_userManager.FindByNameAsync(ApplicationUser.Admin.UserName).Result == null)
                {
                var adminUser = ApplicationUser.Admin;


                // Possible need to check IdentityResult

                var context = app.ApplicationServices.GetService<AppDbContext>();
                var contact = new ContactRecord
                    {
                    AuthorId = "System",
                    CreationDate = DateTime.Now,
                    LastEditDate = DateTime.Now,
                    LastEditorId = "System"
                    };
                context.Add(contact);
                adminUser.Contact = contact;
                _userManager.CreateAsync(adminUser, "123456").Wait();


                _userManager.AddToRoleAsync(adminUser, Roles.Admin).Wait();
                }

            }

        #endregion

        }
    }
