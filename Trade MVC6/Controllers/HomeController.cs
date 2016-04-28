using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;

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
