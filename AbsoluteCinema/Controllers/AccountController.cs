using AbsoluteCinema.Models;
using BusinessLogicLayer;
using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Register(RegisterViewModel model)
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

            return RedirectToAction("Index", "Home");
        }

    }
}
