using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Trade_MVC6.Models.Identity;

namespace Trade_MVC6.Controllers
    {
    public class HomeController : Controller
        {
        public IActionResult Index()
        {
            return View();
            }
        }
    }
