using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Antiforgery;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Trade_MVC6.Models.B2BStrore;
using Trade_MVC6.Models.Identity;
using Trade_MVC6.Services;
using Trade_MVC6.ViewModels.Account;
using System.Linq;

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
        public IActionResult Login(string returnUrl = null)
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
                var authUser = await _userManager.FindByNameAsync(model.Login) ?? 
                               await _userManager.FindByEmailAsync(model.Login);

                if (authUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Ошибка аутентификации.");
                    return View(model);
                }
                 
                var result = await _signInManager.PasswordSignInAsync(authUser, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                    {
                    _logger.LogInformation(1, "User logged in.");

                    if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);

                    return RedirectToAction("Index", "Home");
                    }
                if (result.IsLockedOut)
                    {
                    _logger.LogWarning(2, "Аккаунт заблокирован.");
                    return View("_AccountLocked");
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

            return PartialView("_SuccessRegistration", model.Email);
            }

        // GET: /Account/ConfirmEmail
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
            {
            if (userId == null || code == null)
                {
                return View("_ErrorEmailConfirmation");
                }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                {
                return View("_ErrorEmailConfirmation");
                }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "_SuccessEmailConfirmation" : "_ErrorEmailConfirmation");
            }


        //
        // GET: /Account/ForgotPassword
        [HttpGet, AllowAnonymous]
        public IActionResult ForgotPassword()
            {
            return View();
            }

        //
        // POST: /Account/ForgotPassword
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
            {
            if (ModelState.IsValid)
                {
                var user = await _userManager.FindByNameAsync(model.Email);

                if (user == null)
                    {
                    ModelState.AddModelError("", "Учетная запись с указанным Email не обнаружена.");
                    // Don't reveal that the user does not exist or is not confirmed
                    return PartialView();
                    }
                if (!(await _userManager.IsEmailConfirmedAsync(user)))
                    {
                    ModelState.AddModelError("", "Учетная запись с указанным Email не подтверждена.");
                    // Don't reveal that the user does not exist or is not confirmed
                    return PartialView();
                    }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code },
                protocol: HttpContext.Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Восстановление пароля",
                   "Для сброса пароля нажмите на <a href=\"" + callbackUrl + "\">ссылку</a>");
                return PartialView("_PasswordResetSend", model.Email);
                }
            return PartialView(model);
            }

        //
        // GET: /Account/ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
            {
            return code == null ? View("_ErrorChangePassword") : View(new ResetPasswordViewModel { Code = code});
            }

        //
        // POST: /Account/ResetPassword
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
            {
            if (!ModelState.IsValid)
                {
                return PartialView(model);
                }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
                {
                // Don't reveal that the user does not exist
                return PartialView("_ErrorChangePassword");
                }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
                {
                return PartialView("_SuccessPasswordChange");
                }
            return PartialView("_ErrorChangePassword");
            }

        [HttpGet, Authorize]
        public async Task<IActionResult> Profile(string returnUrl)
        {
            var currentUser = await _userManager.Users.Include(b => b.Contact).FirstAsync(u => u.UserName == User.Identity.Name);
            var currentProfileViewModel = new ProfileViewModel();
            _mapper.Map(currentUser, currentProfileViewModel);
            ViewData["returnUrl"] = returnUrl;
            return PartialView(currentProfileViewModel);
        }


        }
    }
