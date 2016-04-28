using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Trade_MVC6.Models.B2BStrore;
using Trade_MVC6.Models.Identity.AccountDetails;
using Trade_MVC6.Models._1C;

namespace Trade_MVC6.Models.Identity
    {
    public static class IdentityExtensions
        {
        #region Configuration

        private static RoleManager<IdentityRole> _roleManager;
        private static UserManager<ApplicationUser> _userManager;

        public static void EnsureRolesCreated(this IApplicationBuilder app)
            {
            var context = app.ApplicationServices.GetServices<ApplicationDbContext>();
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
                _userManager.CreateAsync(adminUser, "123456").Wait();


                _userManager.AddToRoleAsync(adminUser, Roles.Admin).Wait();
                }

            }

        #endregion

        #region User Operations
        public static Task Activate1CAccessAsync(this UserManager<ApplicationUser> usermanager, ApplicationUser user, User1C user1C)
        {
            user.Account1CId = user1C.Id;
            return usermanager.AddToRoleAsync(user, Roles.User1C);
        }

        public static Task Deactivate1CAccessAsync(this UserManager<ApplicationUser> usermanager, ApplicationUser user)
            {
            user.Account1CId = null;
            return usermanager.RemoveFromRoleAsync(user, Roles.User1C);
            }
        #endregion
        }
    }
