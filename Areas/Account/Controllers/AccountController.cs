using Diplomm.Areas.Account.Models;
using Diplomm.Data;
using Diplomm.Models.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Diplomm.Areas.Account.Controllers
{
    [Area("Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<EmployeesTable> _userManager;
        private readonly SignInManager<EmployeesTable> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<EmployeesTable> manager, SignInManager<EmployeesTable> signInManager)
        {
            _userManager = manager;
            _signInManager = signInManager;

        }

        [Route("{area}/{action}")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return LocalRedirect("/");
            }
            else
            {
                ModelState.AddModelError("", $"Задан неверный логин или пароль");
            }
            return View("Login");
        }

        [Route("{area}/{action}")]
        [HttpGet]
        public IActionResult SignIn()
        {
            return View("Login");
        }



        [Route("{area}/{action}")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Route("{area}/{action}")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = model.GetUser();
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
        }

        [Route("{area}/{action}")]
        [HttpGet]

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect("/");
        }
    }
}
