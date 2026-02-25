using AbsoluteCinema.Models;
using BusinessLogicLayer;
using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace AbsoluteCinema.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService UserService;
        public AccountController(IUserService userService)
        {
            this.UserService = userService;
        }
        
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = UserService.RegisterUser(
                model.Username,
                model.Email,
                model.Password
            );

            if (result != "Success")
            {
                ModelState.AddModelError("", result);
                return View(model);
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, model.Username)
       
    };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
            );

            return RedirectToAction("Index", "Home");
        }
        //Log IN
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = UserService.Authenticate(
                model.UserName,
                model.Password
            );

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }

            // Sign in using cookie auth
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
        };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
            );

            return RedirectToAction("Index", "Home");
        }
       // [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction("Index", "Home");
        }

    }
}
