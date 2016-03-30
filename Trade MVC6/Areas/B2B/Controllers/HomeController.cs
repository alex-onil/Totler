using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace Trade_MVC6.Areas.B2B.Controllers
{
    [Area("b2b")]
    [Authorize]
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
