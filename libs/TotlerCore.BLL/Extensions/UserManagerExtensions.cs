using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Totler1C.DAL.Models;
using TotlerRepository.Models.Identity;

namespace TotlerCore.BLL.Extensions
{
    public static class UserManagerExtensions
    {
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
        }
}
