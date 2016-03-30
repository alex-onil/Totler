using Microsoft.AspNet.Mvc;

namespace Trade_MVC5.Areas.Login.Controllers
{
    [Area("Login")]
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index(string ReturnUrl)
        {
            return View(ReturnUrl);
        }
    }
}
