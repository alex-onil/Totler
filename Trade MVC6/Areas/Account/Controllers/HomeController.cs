using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Trade_MVC6.Models.Identity;
using Trade_MVC6.ViewModels.Login;

namespace Trade_MVC6.Areas.Login.Controllers
    {
    [Area("Account")]
    public class HomeController : Controller
        {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;



        public HomeController(SignInManager<ApplicationUser> signInManager,
                            UserManager<ApplicationUser> userManager,
                            ILoggerFactory loggerFactory)
            {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<HomeController>();

            }

        // GET: /Login/Home/Index
        [HttpGet]
        public IActionResult Login(string returnUrl)
            {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
            }

        // POST: /Login/Home/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
            {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
                {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                    {
                    _logger.LogInformation(1, "User logged in.");

                    if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);

                    RedirectToAction("Index", "Home", new { area = "" });
                    }
                if (result.IsLockedOut)
                    {
                    _logger.LogWarning(2, "Аккаунт заблокирован.");
                    //return View("Lockout");
                    }
                ModelState.AddModelError(string.Empty, "Ошибка аутентификации.");
                return View(model);
                }
            return View(model);
            }

        //
        // POST: /Account/Home/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
            {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction("Index", "Home", new {area = ""});
            }
        }
    }
