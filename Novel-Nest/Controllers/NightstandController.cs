using Microsoft.AspNetCore.Mvc;
using Models;
using Novel_Nest.Models;
using Novel_Nest_Core;
using static System.Reflection.Metadata.BlobBuilder;

namespace Novel_Nest.Controllers
{
	public class NightstandController : Controller
	{
		private BookLogic _bookLogic;
		public NightstandController()
		{
			_bookLogic = new BookLogic();
		}

		public IActionResult Index()
		{
			var nightstandBooks = _bookLogic.GetNightstandBooks();
			var model = new SpecifyBookDetailsViewModel
			{
				NightstandBooks = nightstandBooks
			};
			return View(model);
		}

		[HttpGet]
		public IActionResult AddBookToNightstand()
		{
			var books = _bookLogic.GetBooks();

			var model = new SpecifyBookDetailsViewModel
			{
				Books = books
			};
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> AddBookToNightstand(NightstandBookDTO nightstandBook)
		{
			if (ModelState.IsValid)
			{
				bool success = await _bookLogic.AddBookToNightstandAsync(nightstandBook);

				if (success)
				{
					
					return RedirectToAction("Index", "Nightstand");
				}
				else
				{
					ViewBag.ErrorMessage = "Failed to add book to nightstand. Please try again.";
					return View("ErrorView", nightstandBook);
				}
			}
			else
			{
				return View("AddBookToNightstand", nightstandBook);
			}
		}


	}

}

