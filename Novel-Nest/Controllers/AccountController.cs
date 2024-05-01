using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Novel_Nest_Core;
using Novel_Nest_DAL;
using System;
using Novel_Nest.Models;



namespace Novel_Nest.Controllers
{
	public class AccountController : Controller
	{

		private UserLogic _userLogic;
	

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
