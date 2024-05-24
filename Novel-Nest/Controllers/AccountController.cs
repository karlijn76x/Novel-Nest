using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Novel_Nest_Core;
using System;
using Microsoft.AspNetCore.Http;
using Novel_Nest.Models;
using System.Transactions;
using Novel_Nest_DAL;



namespace Novel_Nest.Controllers
{
	public class AccountController : Controller
	{

		private UserLogic _userLogic;

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
			
            var (isAuthenticated, Name, Id) = await _userLogic.AuthenticateUser(email, password);

            if (isAuthenticated)
            {
                HttpContext.Session.SetInt32("UserId", Id);
                HttpContext.Session.SetString("UserName", Name);
                return RedirectToAction("Index", "Bookshelf");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid login attempt.";
                return View();
            }
        }

        [HttpPost]
		public async Task<IActionResult> CreateAccount(UserModelDTO userModel)
		{
			if (ModelState.IsValid)
			{

				bool success = true;

				if (success)
				{
					Console.WriteLine("success");

					_userLogic = new UserLogic();
					_userLogic.CreateUser(userModel);
					return RedirectToAction("Index", "Bookshelf");
				}
				else
				{
					Console.WriteLine("failed");


					ViewBag.ErrorMessage = "Failed to create account. Please try again.";
					return View("ErrorView", userModel); 
				}
			}
			else
			{
				Console.WriteLine("failed 2");
				return View("CreateAccount", userModel);
			}
		}

	}
}
