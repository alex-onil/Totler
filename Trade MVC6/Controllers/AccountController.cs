using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Trade_MVC6.Models.B2BStrore;
using Trade_MVC6.Models.Identity;
using Trade_MVC6.Services;
using Trade_MVC6.ViewModels.Account;

namespace Trade_MVC6.Controllers
    {
    public class AccountController : Controller
        {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly B2BDbContext _dbContext;

        public AccountController(SignInManager<ApplicationUser> signInManager,
                            UserManager<ApplicationUser> userManager,
                            ILoggerFactory loggerFactory, 
                            IEmailSender emailSender, 
                            IMapper mapper, 
                            B2BDbContext dbContext)
            {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<AccountController>();

            }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string returnUrl)
            {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
            }


        // POST: /Account/Login
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

                    RedirectToAction("Index", "Home");
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

        // GET: /Acoount/LogOut
        [HttpGet]
        public async Task<IActionResult> LogOut()
            {
            if (User.Identity.IsAuthenticated)
                await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction("Index", "Home", new { area = "" });
            }

        //
        // POST: /Account/LogOff
        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public async Task<IActionResult> LogOff()
            {
            if (User.Identity.IsAuthenticated)
                await _signInManager.SignOutAsync();

            _logger.LogInformation(4, "User logged out.");
            //return new HttpStatusCodeResult(200);
            return RedirectToAction("Index", "Home");
            }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
            {
            return View(new RegisterViewModel());
            }

        // POST: /Account/Register[
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
            {
            if (!ModelState.IsValid) return PartialView();

            var user = new ApplicationUser { Access1C = false };
            _mapper.Map(model, user);
            _dbContext.Add(user.Contact, User);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                {

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code },
                    protocol: HttpContext.Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Подтверждение email учётной записи",
                    "Подтвердите Вашу учётную запись нажатием <a href=\"" + callbackUrl + "\">ссылки</a>");
                //await _signInManager.SignInAsync(user, isPersistent: false);
                }

            return PartialView("SuccessRegistration", model.Email);
        }

        // GET: /Account/ConfirmEmail
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
            {
            if (userId == null || code == null)
                {
                return View("ErrorEmailConfirmation");
                }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                {
                return View("ErrorEmailConfirmation");
                }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "SuccessEmailConfirmation" : "ErrorEmailConfirmation");
            }

        }
    }
