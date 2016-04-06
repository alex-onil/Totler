using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Trade_MVC6.Models.Identity;
using Trade_MVC6.ViewModels.Home;

namespace Trade_MVC6.Controllers
    {
    public class HomeController : Controller
        {
        public IActionResult Index()
        {
            return View(new UserStatusViewModel
                {
                IsAuthenticated = User.Identity.IsAuthenticated,
                IsAdmin = User.Identity.IsAuthenticated && User.IsInRole(Roles.Admin)
                });
            }
        }
    }
