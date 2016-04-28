using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Trade_MVC6.Attributes;
using Trade_MVC6.Models.Identity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Trade_MVC6.Controllers
{
    [Authorize(Roles = "Admin"), AjaxValidate]
    public class AdminController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var a = HttpContext;
            return PartialView();
        }
    }
}
