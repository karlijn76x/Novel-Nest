using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Novel_Nest_Core;
using Novel_Nest.Models;
using System.Threading.Tasks;

namespace Novel_Nest.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var (isAuthenticated, Name, Id) = await _userService.AuthenticateUserAsync(email, password);

            if (isAuthenticated)
            {
                HttpContext.Session.SetInt32("UserId", Id);
                HttpContext.Session.SetString("UserName", Name);
                return RedirectToAction("Index", "Bookshelf");
            }
            else
            {
                Console.WriteLine("Invalid login attempt for email: " + email);
                ViewBag.ErrorMessage = "Invalid login attempt.";
                return RedirectToAction("LoginPage", "Home");
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(UserModelDTO userModel)
        {
            if (ModelState.IsValid)
            {
                bool success = await _userService.CreateUserAsync(userModel);

                if (success)
                {
                    return RedirectToAction("LoginPage", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to create account. Please try again.";
                    return View("ErrorView", userModel);
                }
            }
            else
            {
                return View("CreateAccount", userModel);
            }
        }
    }
}
