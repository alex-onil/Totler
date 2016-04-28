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
using Microsoft.AspNet.Identity.EntityFramework;
using Trade_MVC6.Attributes;
using Trade_MVC6.Helpers;

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
        #region Log In/Out

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

        ////
        //// POST: /Account/LogOff
        //[HttpPost, ValidateAntiForgeryToken, Authorize]
        //public async Task<IActionResult> LogOff()
        //    {
        //    if (User.Identity.IsAuthenticated)
        //        await _signInManager.SignOutAsync();

        //    _logger.LogInformation(4, "User logged out.");
        //    //return new HttpStatusCodeResult(200);
        //    return RedirectToAction("Index", "Home");
        //    }

        // GET: /Account/Register
        //[HttpGet]
        //public IActionResult Register()
        //    {
        //    return View(new RegisterViewModel());
        //    }

        // POST: /Account/Register[

        #endregion

        [HttpPost, ValidateHeaderAntiForgeryToken, Route("/Account/Register"), AjaxValidate]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
            {
            if (!model.IsValid) return HttpBadRequest(model.ValidationMessages().Select(n => n.ErrorMessage));
            var user = new ApplicationUser();
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
                return Ok();
                }

            return HttpBadRequest(new[] { "Ошибка базы данных при создании учетной записи." });
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
                var user = await _userManager.FindByNameAsync(model.Email) ??
                            await _userManager.FindByNameAsync(model.Email);

                if (user == null)
                    {
                    ModelState.AddModelError("", "Учетная запись не обнаружена.");
                    // Don't reveal that the user does not exist or is not confirmed
                    return View();
                    }
                if (!(await _userManager.IsEmailConfirmedAsync(user)))
                    {
                    ModelState.AddModelError("", "Email учетной записи не подтвержден. Учетная запись с указанным Email не подтверждена.");
                    // Don't reveal that the user does not exist or is not confirmed

                    var emlCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var emlCallbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = emlCode },
                        protocol: HttpContext.Request.Scheme);
                    await _emailSender.SendEmailAsync(user.Email, "Подтверждение email учётной записи",
                            "Подтвердите Вашу учётную запись нажатием <a href=\"" + emlCallbackUrl + "\">ссылки</a>");

                    return View("_ChangePassSendEmailCfrm");
                    }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, email = user.Email, code = code },
                protocol: HttpContext.Request.Scheme);
                await _emailSender.SendEmailAsync(user.Email, "Восстановление пароля",
                   "Для сброса пароля нажмите на <a href=\"" + callbackUrl + "\">ссылку</a>");
                return View("_PasswordResetSend");
                }
            return PartialView(model);
            }

        //
        // GET: /Account/ResetPassword
        [HttpGet, AllowAnonymous]
        public IActionResult ResetPassword(string code = null, string email = null)
            {
            return (code == null || email == null) ?
                    View("_ErrorChangePassword") : View(new ResetPasswordViewModel { Code = code, Email = email });
            }

        //
        // POST: /Account/ResetPassword
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
            {
            if (!ModelState.IsValid)
                {
                return View(model);
                }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                {
                // Don't reveal that the user does not exist
                return View("_ErrorChangePassword");
                }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
                {
                return View("_SuccessPasswordChange");
                }
            return View("_ErrorChangePassword");
            }

        // GET: /Account/Profile
        [HttpGet, Authorize, Route("Account/Profile"), AjaxValidate]
        public async Task<IActionResult> Profile(string returnUrl)
            {
            var currentUser = await _userManager.Users.Include(b => b.Contact).FirstAsync(u => u.UserName == User.Identity.Name);
            var currentProfileViewModel = new ProfileViewModel();
            _mapper.Map(currentUser, currentProfileViewModel);
            currentProfileViewModel.Access1C = User.IsInRole(Roles.User1C);

            return PartialView(currentProfileViewModel);
            }

        // POST: /Account/Profile
        [HttpPost, Authorize, ValidateHeaderAntiForgeryToken, Route("Account/Profile")]
        public async Task<IActionResult> SaveProfile([FromBody] ProfileViewModel model)
            {

            if (!model.IsValid)
                {
                return HttpBadRequest(model.ValidationMessages().Select(m => m.ErrorMessage));
                }

            var currentUser = await _userManager.Users.Include(b => b.Contact).FirstAsync(u => u.UserName == User.Identity.Name);

            if (currentUser == null) return HttpBadRequest(new[] { "Пользователь не найден." });

            if (model.CompanyName != currentUser.CompanyName && User.IsInRole(Roles.User1C))
                {
                await _userManager.RemoveFromRoleAsync(currentUser, Roles.User1C);
                }
            _mapper.Map(model, currentUser);
            var result = await _userManager.UpdateAsync(currentUser);

            if (result.Succeeded) return Ok();

            return HttpBadRequest(new[] { "В процессе обновления пользователя произошла ошибка." });
            }

        // POST: /Account/ReSendEmailConfirmation
        [HttpPost, Authorize, ValidateHeaderAntiForgeryToken, Route("Account/ReSendEmailConfirmation")]
        public async Task<IActionResult> EmailConfimation()
            {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (currentUser.EmailConfirmed) return HttpBadRequest();
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(currentUser);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = currentUser.Id, code = code },
                protocol: HttpContext.Request.Scheme);
            await _emailSender.SendEmailAsync(currentUser.Email, "Подтверждение email учётной записи",
                    "Подтвердите Вашу учётную запись нажатием <a href=\"" + callbackUrl + "\">ссылки</a>");
            return Ok();
            }

        // POST: /Account/EmailChangeRequest
        [HttpPost, Authorize, ValidateHeaderAntiForgeryToken, Route("Account/EmailChangeRequest")]
        public async Task<IActionResult> ChangeEmail(string newEmail)
            {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

            var code = await _userManager.GenerateChangeEmailTokenAsync(currentUser, newEmail);

            var callbackUrl = Url.Action("EmailChangeConfirmation", "Account", new { userId = currentUser.Id, code = code, newEmail = newEmail },
                protocol: HttpContext.Request.Scheme);

            await _emailSender.SendEmailAsync(currentUser.Email, "Подтверждение изменения Email",
                    "Подтвердите изменения Email Вашей учётной записи нажатием <a href=\"" + callbackUrl + "\">ссылки</a>");

            return Ok();
            }

        // GET: /Account/EmailChangeConfirmation
        [HttpGet, Route("Account/EmailChangeConfirmation")]
        public async Task<IActionResult> EmailChangeConfirmation(string userId, string code, string newEmail)
            {
            var currentUser = await _userManager.FindByIdAsync(userId);

            if (currentUser == null) return View("_ErrorEmailChange");

            var result = await _userManager.ChangeEmailAsync(currentUser, newEmail, code);

            if (result.Succeeded)
                {
                return View("_SuccessEmailChange");
                }

            return View("_ErrorEmailChange");

            }

        [HttpGet, ValidateHeaderAntiForgeryToken, Route("/Account/CheckUser"), AjaxValidate(RedirectToHome = true)]
        public Task<JsonResult> CheckUserName(string userName) =>
            Task.Run(() => Json(_userManager.Users.FirstOrDefault(u => u.UserName.Equals(userName)) == null));

        [HttpGet, ValidateHeaderAntiForgeryToken, Route("/Account/CheckEmailDuplicate"), AjaxValidate(RedirectToHome = true)]
        public Task<JsonResult> CheckEmailDuplicate(string email) =>
            Task.Run(() => Json(_userManager.Users.FirstOrDefault(u => u.NormalizedEmail.Equals(email.ToUpper())) == null));

        }
    }
