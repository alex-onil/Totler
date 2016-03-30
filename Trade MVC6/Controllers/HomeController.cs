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
