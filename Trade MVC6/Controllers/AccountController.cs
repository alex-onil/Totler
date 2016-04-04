﻿using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Trade_MVC6.Models;
using Trade_MVC6.Models.Identity;
using Trade_MVC6.ViewModels.Account;

namespace Trade_MVC6.Controllers
    {
    public class AccountController : Controller
        {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;



        public AccountController(SignInManager<ApplicationUser> signInManager,
                            UserManager<ApplicationUser> userManager,
                            ILoggerFactory loggerFactory)
            {
            _signInManager = signInManager;
            _userManager = userManager;
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
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return PartialView();

            return RedirectToAction("Index", "Home");
        }

        }
    }
