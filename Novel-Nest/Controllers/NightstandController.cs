using Microsoft.AspNetCore.Mvc;
using Novel_Nest.Models;
using Novel_Nest_Core;

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
			return View();
		}

		public IActionResult AddBookToNightstand()
		{
			var books = _bookLogic.GetBooks();

			var model = new SpecifyBookDetailsViewModel
			{
				Books = books
			};
			return View(model);
		}
	}
}
